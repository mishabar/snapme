﻿using SNAPME.Live.Data.Entities;
using SNAPME.Live.Data.Repositories;
using SNAPME.Live.Services.Interfaces;
using SNAPME.Live.Services.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<SimpleProductToken>> GetCommunityProducts(long[] communityIds)
        {
            return (await _productRepository.GetCommunityProducts(communityIds)).Select(p => new SimpleProductToken(p));
        }


        public async Task<FullProductToken> GetProduct(string name)
        {
            var product = await _productRepository.GetProduct(name);
            return product == null ? null : new FullProductToken(product);
        }


        public async Task<FullProductToken> GetProduct(long productId)
        {
            var product = await _productRepository.GetProduct(productId);
            return product == null ? null : new FullProductToken(product);
        }


        public async Task<long> SaveProduct(FullProductToken fullProductToken)
        {
            return 1;
        }


        public async Task<bool> UpdateProduct(FullProductToken request)
        {
            return await _productRepository.UpdateProduct(request.AsProduct());
        }


        public async Task<SnapToken> JoinSale(long productId, string userId, string imageUrl, string userName, string customerId, string chargeId,
            string shippingFirstName, string shippingLastName, string shippingAddress, string shippingAddress2, string shippingCity,
            string shippingState, string shippingZip)
        {
            var snap = await _productRepository.Snap(productId, new Snap
            {
                external_user_id = customerId,
                user_id = userId,
                image_url = imageUrl,
                name = userName,
                charge_id = chargeId,
                quantity = 0,
                product_amount = 0,
                shipping_amount = 0,
                snapped_at = DateTime.Now,
                address = new DestinationShippingInfo
                {
                    first_name = shippingFirstName,
                    last_name = shippingLastName,
                    address = shippingAddress,
                    address2 = shippingAddress2,
                    city = shippingCity,
                    state = shippingState,
                    zip = shippingZip
                }
            });
            
            return new SnapToken(snap);
        }


        public async Task<IEnumerable<BaseProductToken>> GetProducts()
        {
            return (await _productRepository.GetProducts()).Select(p => new BaseProductToken(p));
        }


        public async Task<IEnumerable<SaleToken>> GetSales(long id)
        {
            return (await _productRepository.GetSales(id)).Select(s => new SaleToken(s, true));
        }


        public async Task<bool> SaveSale(long product_id, SaleToken sale)
        {
            return (await _productRepository.SaveSale(product_id, new SaleDetails
            {
                sale_id = sale.sale_id,
                target = sale.target,
                state = sale.state,
                sale_type = sale.sale_type,
                required_snaps = sale.required_snaps,
                stock = sale.stock,
                starts_on = sale.starts_on,
                ends_on = sale.ends_on
            }));
        }
    }
}
