using AutoMapper;
using WebApiTienda.Context;
using WebApiTienda.Helpers;
using WebApiTienda.Models;
using WebApiTienda.Services;

namespace WebApiTienda.BO
{
    public class PurchaseBO
    {

        private PurchaseDataService _purchaseDataService;
        private PurchaseContext _purchaseContext;
        private readonly IMapper _mapper;

        public PurchaseBO(PurchaseContext context, IMapper mapper) {
            _purchaseContext = context;
            _purchaseDataService = new PurchaseDataService(_purchaseContext);
            _mapper = mapper;
        }

        public int SavePurchase(PurchaseRequest purchaseRequest)
        {
            int newPurchase = 0;
            List<PurchaseDetail> purchaseDetailsCreated = new List<PurchaseDetail>();

            try
            {
                // Crea el registro de la compra
                newPurchase = CreatePurchase(purchaseRequest.Total);
                // Crea el detalle de la compra
                purchaseDetailsCreated = CreatePurchaseDetail(newPurchase, purchaseRequest.Products.ToList());

                return newPurchase;
            }
            catch (Exception ex)
            {
                // Si alcanzó a crear el registro de la compra
                if (newPurchase != 0)
                {
                    _purchaseContext.ChangeTracker.Clear();           // Limipia los cambios
                    _purchaseDataService.DeletePurchase(newPurchase); // Elimina el registro de la compra creado
                }
                return -1;
            }
        }
        
        private int CreatePurchase(int total)
        {
            // Crea el registro de la compra
            int idNewPurchase = _purchaseDataService.CreatePurchase(total);
            // retorna el id de la compra creada
            return idNewPurchase;
        }

        private List<PurchaseDetail> CreatePurchaseDetail(int idPurchase, List<PurchaseDetailRequest> products)
        {
            // Lista de detalles creados
            List<PurchaseDetail> purchaseDetailsCreated =   new List<PurchaseDetail>();

            // Guarda los detalles
            purchaseDetailsCreated = _purchaseDataService.CreatePurchaseDetail(idPurchase, _mapper.Map<List<PurchaseDetail>>(products));
            
            return purchaseDetailsCreated;
        }

        public void DeletePurchase(int idPurchase)
        {
            _purchaseDataService.DeletePurchase(idPurchase);
        }

        public PurchaseRequest GetPurchase(int idPurchase)
        {
            PurchaseRequest response = new PurchaseRequest();

            // Obtiene el registro de la compra
            var purchase = _purchaseDataService.GetPurchase(idPurchase);

            if (purchase != null)
            {
                //Obtiene el detalle de la compra
                var purchaseDetails = _purchaseDataService.GetPurchaseDetails(idPurchase);

                response = _mapper.Map<PurchaseRequest>(purchase);
                response.Products = _mapper.Map<PurchaseDetailRequest[]>(purchaseDetails);
            }

            return response;
        }
    }
}
