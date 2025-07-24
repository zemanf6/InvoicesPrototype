﻿/*  _____ _______         _                      _
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

using Invoices.Data.Entities.Enums;
using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    /// <summary>
    /// DTO (Data Transfer Object) třída pro přenos dat o osobě přes API.
    /// Odděluje vnější datovou strukturu (např. JSON pro frontend) od vnitřní databázové entity.
    /// </summary>
    public class PersonDto
    {
        // Upravíme název vlastnosti v JSON výstupu (např. "_id" místo "id") kvůli React klientovi
        [JsonPropertyName("_id")]
        public int Id { get; set; }

        public string Name { get; set; } = "";
        public string IdentificationNumber { get; set; } = "";
        public string TaxNumber { get; set; } = "";
        public string AccountNumber { get; set; } = "";
        public string BankCode { get; set; } = "";
        public string Iban { get; set; } = "";
        public string Telephone { get; set; } = "";
        public string Mail { get; set; } = "";
        public string Street { get; set; } = "";
        public string Zip { get; set; } = "";
        public string City { get; set; } = "";

        // Enum Country se v JSONu serializuje jako string
        public Country Country { get; set; }

        public string Note { get; set; } = "";
    }
}