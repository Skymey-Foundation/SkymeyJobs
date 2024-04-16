using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using SkymeyJobsLibs.Models.ActualPrices;
using SkymeyJobsLibs.Models.ActualPrices.Binance;
using SkymeyJobsLibs.Models.ActualPrices.Okex;
using SkymeyJobsLibs.Models.Tickers;
using SkymeyJobsLibs.Models.Tickers.Crypto;
using SkymeyJobsLibs.Models.Tickers.Crypto.Binance;
using SkymeyJobsLibs.Models.Tickers.Tinkoff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SkymeyJobsLibs.Data
{
    public class ApplicationContext : DbContext
    {
        #region CRYPTO
        public DbSet<BinanceCurrentPrice> BinanceCurrentPrices { get; init; }
        public DbSet<OkexCurrentPrices> OkexCurrentPricesView { get; init; }
        public DbSet<CurrentPrices> CurrentPrices { get; init; }
        public DbSet<CryptoTickers> CryptoTickers { get; init; }
        public DbSet<Symbol> BinanceTickers { get; init; }
        #endregion

        #region STOCKS
        public DbSet<TickerList> TickerList { get; init; }
        public DbSet<TinkoffSharesInstrument> Shares { get; init; }

        #endregion

        #region BONDS
        public DbSet<TinkoffBondInstrument> Bonds { get; init; }
        #endregion

        #region ETFS
        public DbSet<TinkoffETFInstrument> Etfs { get; init; }
        #endregion

        #region CURRENCIES
        public DbSet<TinkoffCurrenciesInstrument> Currencies { get; init; }
        #endregion

        #region FUTURES
        public DbSet<TinkoffFuturesInstrument> Futures { get; init; }
        #endregion

        public static ApplicationContext Create(IMongoDatabase database) =>
            new(new DbContextOptionsBuilder<ApplicationContext>()
                .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
                .Options);
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region CRYPTO
            modelBuilder.Entity<BinanceCurrentPrice>().ToCollection("crypto_current_binance_prices");
            modelBuilder.Entity<Symbol>().ToCollection("crypto_binance_tickers");
            modelBuilder.Entity<CryptoTickers>().ToCollection("crypto_tickers");
            modelBuilder.Entity<OkexCurrentPrices>().ToCollection("crypto_current_okex_prices");
            modelBuilder.Entity<CurrentPrices>().ToCollection("crypto_current_prices");
            #endregion

            #region STOCKS
            modelBuilder.Entity<TickerList>().ToCollection("stock_tickerlist");
            modelBuilder.Entity<TinkoffSharesInstrument>().ToCollection("stock_shareslist");
            #endregion

            #region BONDS
            modelBuilder.Entity<TinkoffBondInstrument>().ToCollection("stock_bondlist");
            #endregion

            #region ETFS
            modelBuilder.Entity<TinkoffETFInstrument>().ToCollection("stock_etflist");
            #endregion

            #region CURRENCIES
            modelBuilder.Entity<TinkoffCurrenciesInstrument>().ToCollection("stock_currencieslist");
            #endregion

            #region FUTURES
            modelBuilder.Entity<TinkoffFuturesInstrument>().ToCollection("stock_futureslist");
            #endregion
        }
    }
}
