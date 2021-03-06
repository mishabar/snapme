﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data.Repositories;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens;
using SNAPME.Tokens.Admin;

namespace SNAPME.Services
{
    public class SellerService : ISellerService
    {
        private ISellerRepository _sellerRepository;
        private IProductRepository _productRepository;

        public SellerService(ISellerRepository sellerRepository, IProductRepository productRepository)
        {
            _sellerRepository = sellerRepository;
            _productRepository = productRepository;
        }

        public Tokens.Admin.SellerToken Save(Tokens.Admin.SellerToken token)
        {
            return _sellerRepository.Save(token.AsSeller()).AsToken();
        }


        public IEnumerable<SellerToken> GetAll()
        {
            return _sellerRepository.GetAll().Select(s => s.AsToken());
        }


        public SellerToken GetById(string id)
        {
            return _sellerRepository.GetById(id).AsToken();
        }


        public IEnumerable<Tokens.ProductToken> GetAllProducts(string id)
        {
            return _productRepository.GetAllForSeller(id).Select(p => p.AsToken());
        }


        public ProductToken GetProduct(string id)
        {
            return _productRepository.GetById(id).AsToken();
        }


        public void SaveProduct(ProductToken product)
        {
            _productRepository.Save(product.AsProduct());
        }


        public void SaveProductImage(string id, string image, int idx)
        {
            _productRepository.SaveImage(id, image, idx);
        }


        public void Archive(string id)
        {
            _sellerRepository.Archive(id);
        }


        public void CheckProduct(string id)
        {
            _productRepository.CheckProduct(id);
        }
    }
}
