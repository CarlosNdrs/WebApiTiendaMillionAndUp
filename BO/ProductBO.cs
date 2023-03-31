using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq.Expressions;
using WebApiTienda.DTOs;
using WebApiTienda.Helpers;
using WebApiTienda.Services;

namespace WebApiTienda.BO
{
    public class ProductBO
    {
        private ExternalDataService externalDataService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProductBO(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            externalDataService = new ExternalDataService(_configuration);
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetProducts(ProductRequest request)
        {
            // Listado de productos filtrados
            List<ProductDTO> filteredProducts;

            // Obtiene los productos del API FakeStore
            var fakeStoreProductos = await externalDataService.GetFakeStoreProducts();

            // Obtiene los productos del API DummyJson
            var DummyJsonProducts = await externalDataService.GetDummyJsonProducts();

            // Mapea los resultados al DTO
            List<ProductDTO> fakeStoreProdsDto = _mapper.Map<List<ProductDTO>>(fakeStoreProductos);
            List<ProductDTO> dummyJsonProdsDto = _mapper.Map<List<ProductDTO>>(DummyJsonProducts);

            // COnsolida las 2 listas de productos en 1 sola
            var allProducts = fakeStoreProdsDto.Concat(dummyJsonProdsDto);

            //Predicados para la consulta y los filtros
            Expression<Func<ProductDTO, bool>> predicate = p => p.Id != null;
            Expression<Func<ProductDTO, bool>> predicateSearch = null;
            Expression<Func<ProductDTO, bool>> predicateCategory = null;
            Expression<Func<ProductDTO, bool>> predicateRange = null;
            Expression<Func<ProductDTO, object>> orderField = p => p.Price;

            // Si la consula contiene un término de búsqueda
            if (request.Search != null && request.Search != "")
            {
                predicateSearch = p => p.Title.ToLower().Contains(request.Search.ToLower());

                predicate = Expression.Lambda<Func<ProductDTO, bool>>(Expression.AndAlso(
                new SwapVisitor(predicate.Parameters[0], predicateSearch.Parameters[0]).Visit(predicate.Body),
                predicateSearch.Body), predicateSearch.Parameters);
            }

            // Si la consulta está filtrando por categoria
            if (request.Category != null && request.Category != "")
            {
                predicateCategory = p=> p.Category == request.Category;

                predicate = Expression.Lambda<Func<ProductDTO, bool>>(Expression.AndAlso(
                new SwapVisitor(predicate.Parameters[0], predicateCategory.Parameters[0]).Visit(predicate.Body),
                predicateCategory.Body), predicateCategory.Parameters);
            }

            // Si la consulta contiene un rango de precios
            if (request.MinPrice != null && request.MaxPrice != null)
            {
                predicateRange = p => p.Price >= request.MinPrice && p.Price <= request.MaxPrice;

                predicate = Expression.Lambda<Func<ProductDTO, bool>>(Expression.AndAlso(
                new SwapVisitor(predicate.Parameters[0], predicateRange.Parameters[0]).Visit(predicate.Body),
                predicateRange.Body), predicateRange.Parameters);
            }

            // Si la consulta tiene un precio mínimo sin máximo
            if (request.MinPrice != null && request.MaxPrice == null)
            {
                predicateRange = p => p.Price >= request.MinPrice;

                predicate = Expression.Lambda<Func<ProductDTO, bool>>(Expression.AndAlso(
                new SwapVisitor(predicate.Parameters[0], predicateRange.Parameters[0]).Visit(predicate.Body),
                predicateRange.Body), predicateRange.Parameters);
            }

            // Si la consulta tiene un precio máximo sin mínimo
            if (request.MinPrice == null && request.MaxPrice != null)
            {
                predicateRange = p => p.Price <= request.MaxPrice;

                predicate = Expression.Lambda<Func<ProductDTO, bool>>(Expression.AndAlso(
                new SwapVisitor(predicate.Parameters[0], predicateRange.Parameters[0]).Visit(predicate.Body),
                predicateRange.Body), predicateRange.Parameters);
            }

            // Ordenamiento por Precio o Titulo/Nombre Producto
            switch (request.OrderField)
            {
                case "Price":
                    orderField = p => p.Price;
                    break;

                case "Title":
                    orderField = p => p.Title;
                    break;

                default:
                    break;
            }

            // Si el ordenamiento es Ascendente
            if (request.Ascending)
            {
                filteredProducts = allProducts.AsQueryable()
                    .Where(predicate)
                    .OrderBy(orderField)
                    .ToList();
                    
            } else // Si el ordenamiento es Descendente
            {
                filteredProducts = allProducts.AsQueryable()
                    .Where(predicate)
                    .OrderByDescending(orderField)
                    .ToList();
            }


            return filteredProducts;
        }

        public async Task<ProductDTO> GetProduct(string store, int id)
        {
            ProductDTO result = new();

            // Si el producto es de la tienda Fakestore
            if (store == "FakeStore")
            {
                var fakeStoreProducto = await externalDataService.GetFakeStoreProduct(id);
                // Mapea los resultados al DTO
                result = _mapper.Map<ProductDTO>(fakeStoreProducto);

            }
            // Si el producto es de la tienda DummyJson
            else if (store == "DummyJson")
            {
                var dummyJsonProducto = await externalDataService.GetDummyJsonProduct(id);
                // Mapea los resultados al DTO
                result = _mapper.Map<ProductDTO>(dummyJsonProducto);
            }

            return result;

        }
    }
}
