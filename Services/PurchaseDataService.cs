using WebApiTienda.Context;
using WebApiTienda.Models;

namespace WebApiTienda.Services
{
    public class PurchaseDataService
    {
        private PurchaseContext _context;

        public PurchaseDataService(PurchaseContext context)
        {
            _context = context;
        }

        public int CreatePurchase(decimal total)
        {
            // Crea nuevo objeto de compra
            Purchase newPurchase = new Purchase()
            {
                Date_Pruchase = DateTime.Now,
                Total = total 
            };

            // Añade al contexto
            _context.Purchase.Add(newPurchase);
            // Guarda los cambios
            _context.SaveChanges();

            // Retorna el id de la compra creada
            return newPurchase.Id;
        }

        public List<PurchaseDetail> CreatePurchaseDetail(int PurchaseId, List<PurchaseDetail> products) {
        
            List<PurchaseDetail> createdDetail = new List<PurchaseDetail>();

            foreach (var product in products)
            {
                // Crea un nuevo objeto por cada detalle/producto
                PurchaseDetail newPurchaseDetail = new PurchaseDetail()
                {
                    IdPurchase = PurchaseId,
                    Store = product.Store,
                    IdProduct = product.IdProduct,
                    Title = product.Title,
                    UnitPrice = product.UnitPrice,
                    Quantity = product.Quantity,
                    TotalProduct = product.TotalProduct
                };

                // Añade al contexto
                _context.PurchaseDetails.Add(newPurchaseDetail);                

                // Aañade a la lista para retornar
                createdDetail.Add(newPurchaseDetail);                
            }

            // Guarda los cambios
            _context.SaveChanges();

            // Retorna la lista de detalles creados
            return createdDetail;
        }

        public void DeletePurchase(int purchaseId)
        {
            // Obtiene el objeo a eliminar
            var purchaseToDel = _context.Purchase.FirstOrDefault(p => p.Id == purchaseId);

            // Si existe
            if (purchaseToDel != null)
            {
                // Lo elimina
                _context.Purchase.Remove(purchaseToDel);
                // Guarda los cambios
                _context.SaveChanges();
            }            
        }

        // Obtiene los datos de un registro de compra
        public Purchase GetPurchase(int purchaseId)
        {
            return _context.Purchase.FirstOrDefault(p => p.Id == purchaseId) as Purchase;
        }

        // Obtiene los detalles de una compra
        public List<PurchaseDetail> GetPurchaseDetails(int purchaseId)
        {
            return _context.PurchaseDetails.Where(l => l.IdPurchase == purchaseId).ToList();
        }
    }
}
