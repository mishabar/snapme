using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data;
using SNAPME.Data.Repositories;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens;

namespace SNAPME.Services
{
    public class SaleService : ISaleService
    {
        private ISaleRepository _saleRepository;
        private IProductRepository _productRepository;

        public SaleService(ISaleRepository saleRepository, IProductRepository productRepository)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
        }

        public Tokens.SaleToken GetActive(string id)
        {
            var sale = _saleRepository.GetById(id, true);
            return sale != null ? sale.AsToken() : null;
        }


        public IEnumerable<SaleToken> GetActive()
        {
            return _saleRepository.GetActive().Select(s => s.AsToken());
        }


        public void SaveSale(SaleToken token)
        {
            var sale = token.AsSale();
            var product = _productRepository.GetById(token.product_id);
            sale.name = product.name;
            sale.summary = product.short_description;

            if (token.id == null)
            {
                sale.active = false;
                sale.drops = new Drop[0];
                sale.retail_price = product.retail_price;
                sale.price = product.retail_price;
                _saleRepository.Create(sale);
                token.id = sale.id;
            }
            else
            {
                _saleRepository.Save(sale);
            }
        }


        public SaleToken GetScheduledSale(string productId)
        {
            var sale = _saleRepository.GetScheduledSale(productId);

            return sale == null ? null : sale.AsToken();
        }


        public void AdjustSalesStatus()
        {
            _saleRepository.AdjustSalesStatus();
        }
    }
}
