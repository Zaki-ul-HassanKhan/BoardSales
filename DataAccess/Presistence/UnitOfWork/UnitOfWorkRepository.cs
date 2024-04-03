﻿using DataAccess.Context;
using DataAccess.Presistence.Product;
using DataAccess.Presistence.User;
using Domain.Repository.Product;
using Domain.Repository.UnitOfWork;
using Domain.Repository.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Presistence.UnitOfWork
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly BoardSalesDbContext _boardSalesDbContext;
        private UserRepository _userRepository;
        private ProductRepository _productRepository;
        public UnitOfWorkRepository(BoardSalesDbContext boardSalesDbContext)
        {
            _boardSalesDbContext = boardSalesDbContext;
            UserRepository = new UserRepository(boardSalesDbContext);
            ProductRepository = new ProductRepository(boardSalesDbContext); 
        }
        public IUserRepository UserRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IUserRepository User => throw new NotImplementedException();

        public IProductRepository Product => throw new NotImplementedException();

        //public IUserRepository User => throw new NotImplementedException();

        //public IProductRepository Product => throw new NotImplementedException();

        public void Dispose()
        {
            _boardSalesDbContext.Dispose();
        }

        public int Save()
        {
           return _boardSalesDbContext.SaveChanges();
        }
    }
}