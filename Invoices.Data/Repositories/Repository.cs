/*  _____ _______         _                      _
 * |_   _|__   __|       | |                    | |
 *   | |    | |_ __   ___| |___      _____  _ __| | __  ___ ____
 *   | |    | | '_ \ / _ \ __\ \ /\ / / _ \| '__| |/ / / __|_  /
 *  _| |_   | | | | |  __/ |_ \ V  V / (_) | |  |   < | (__ / /
 * |_____|  |_|_| |_|\___|\__| \_/\_/ \___/|_|  |_|\_(_)___/___|
 *
 *                      ___ ___ ___
 *                     | . |  _| . |  LICENCE
 *                     |  _|_| |___|
 *                     |_|
 *
 *    REKVALIFIKAČNÍ KURZY  <>  PROGRAMOVÁNÍ  <>  IT KARIÉRA
 *
 * Tento zdrojový kód je součástí profesionálních IT kurzů na
 * WWW.ITNETWORK.CZ
 *
 * Kód spadá pod licenci PRO obsahu a vznikl díky podpoře
 * našich členů. Je určen pouze pro osobní užití a nesmí být šířen.
 * Více informací na http://www.itnetwork.cz/licence
 */

using Invoices.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data.Repositories
{
    /// <summary>
    /// Obecné (generické) úložiště pro práci s entitami v databázi
    /// Poskytuje základní CRUD operace pro libovolný typ T
    /// </summary>
 
   /*
    * BONUS rozšíření: Lze nad rámec zadání implementovat metody GetAll, GetById, SaveChanges, Add
    * asynchronně (async, await)
    * EF poskytuje metody ToListAsync(), FindAsync(), SaveChangesAsync(), AddAsync() 
    */
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T? GetById(int id)
        {
            // Find využívá EF cache – rychlé, ale nelze použít s dalšími podmínkami nebo Include()
            return _dbSet.Find(id);
        }

        public T Add(T entity)
        {
            // Entita se přidá do DbContextu, ale změny je třeba uložit SaveChanges()
            _dbSet.Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool ExistsWithId(int id)
        {
            // AsNoTracking zlepší výkon – entitu nesledujeme
            return _dbSet.AsNoTracking().Any(e => EF.Property<int>(e, "Id") == id);
        }
    }
}