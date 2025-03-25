using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
    }
}