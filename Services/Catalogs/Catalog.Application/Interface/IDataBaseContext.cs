﻿using Catalog.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Interface
{
    public interface IDataBaseContext
    {
       IMongoCollection<Product> Products { get; }
    }
}
