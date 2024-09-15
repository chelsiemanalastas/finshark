using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _db;
        public StockRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _db.Stocks.AddAsync(stock);
            await _db.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _db.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (stockModel == null) 
            {
                return null;
            }

            _db.Stocks.Remove(stockModel);
            await _db.SaveChangesAsync();

            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _db.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _db.Stocks.FindAsync(id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var stockModel = await _db.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (stockModel == null) 
            {
                return null;
            }

            stockModel.Symbol = stockDto.Symbol;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.Purchase = stockDto.Purchase;
            stockModel.LastDiv = stockDto.LastDiv;
            stockModel.Industry = stockDto.Industry;
            stockModel.MarketCap = stockDto.MarketCap;

            await _db.SaveChangesAsync();

            return stockModel;
        }
    }
}