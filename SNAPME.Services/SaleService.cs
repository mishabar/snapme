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
        private IUserPreferencesRepository _userPreferencesRepository;

        public SaleService(ISaleRepository saleRepository, IProductRepository productRepository, IUserPreferencesRepository userPreferencesRepository)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _userPreferencesRepository = userPreferencesRepository;
        }

        public Tokens.SaleToken GetActive(string id, string userId)
        {
            UserPreferences userPreferences = new UserPreferences(userId);
            if (!string.IsNullOrWhiteSpace(userId))
            {
                userPreferences = _userPreferencesRepository.GetById(userId, null);
            }
            var sale = _saleRepository.GetById(id, true);
            return sale != null ? sale.AsToken(userPreferences, _productRepository.GetById(sale.product_id)) : null;
        }


        public IEnumerable<SaleToken> GetAllActive(string userId)
        {
            UserPreferences userPreferences = new UserPreferences(userId);
            if (!string.IsNullOrWhiteSpace(userId))
            {
                userPreferences = _userPreferencesRepository.GetById(userId, null);
            }
            return _saleRepository.GetActive().Select(s => s.AsToken(userPreferences, _productRepository.GetById(s.product_id)));
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


        public SaleToken GetScheduledSale(string productId, string userId)
        {
            var sale = _saleRepository.GetScheduledSale(productId);

            return sale == null ? null : sale.AsToken(string.IsNullOrWhiteSpace(userId) ? null : _userPreferencesRepository.GetById(userId, productId), null);
        }


        public void AdjustSalesStatus()
        {
            _saleRepository.AdjustSalesStatus();
        }


        public SaleToken JoinSale(string userId, string productId)
        {
            var sale = _saleRepository.JoinSale(userId, productId);
            return sale == null ? null : sale.AsToken(null, null);
        }
    }
}
