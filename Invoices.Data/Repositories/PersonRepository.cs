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

using Invoices.Data.Entities;
using Invoices.Data.Repositories.Interfaces;

namespace Invoices.Data.Repositories
{
    /// <summary>
    /// Specifické úložiště pro práci s entitou Person
    /// Obsahuje dodatečné metody, které nejsou součástí obecného repository
    /// </summary>
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Vrátí všechny osoby podle příznaku Hidden
        /// Skryté osoby se běžně nezobrazují (soft delete), ale je možné je načíst
        /// </summary>
        public IEnumerable<Person> GetByHidden(bool hidden)
        {
            return _dbSet.Where(x => x.Hidden == hidden).ToList();
        }
    }
}