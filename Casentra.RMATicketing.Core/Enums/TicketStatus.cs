using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Enums
{
    public enum TicketStatus : int
    {
        [Display(Name = "New")]
        New = 0,
    }

    public enum AdminTicketStatus : int
    {
       
        [Display(Name = "Reception's Confirmation in France")]
        ReceptionsConfirmationInFR = 1,
        [Display(Name = "Reparation in progress")]
        ReparatioInProgress = 2,
        [Display(Name = "Reparation in France")]
        ReparationInFrance = 3,
        [Display(Name = "Shipped from France")]
        ShippedFromFrance = 4,
        [Display(Name = "Need to ship in China")]
        NeedToShipToChina = 5,
        [Display(Name = "Reparation in China")]
        ReparationInChina = 6,
        [Display(Name = "Shipped from China")]
        ShippedFromChina = 7,
        [Display(Name = "Quotation request")]
        QuotationRequest = 8,

    }

    public enum TicketBoard : int
    {
        [Display(Name = "France")]
        France = 1,
        [Display(Name = "China")]
        China = 2,
        
    };

    public enum TicketPriority : int
    {
        [Display(Name = "Low")]
        Low = 1,
        [Display(Name = "Normal")]
        Normal = 2,
        [Display(Name = "High")]
        High = 3,
        [Display(Name = "Urgent")]
        Urgent = 4
    };
         

    public enum ColorList : int
    {
        [Display(Name = "Blanc")]
        Blanc = 1,
        [Display(Name = "Noir")]
        Noir = 2,
        [Display(Name = "Bleu")]
        Bleu = 3,
        [Display(Name = "Vert")]
        Vert = 4,
        [Display(Name = "Rose")]
        Rose = 5,
        [Display(Name = "Jaune")]
        Jaune = 6,
        [Display(Name = "Champagne")]
        Champagne = 7,
        [Display(Name = "Rose Gold")]
        RoseGold = 8
    };
    public enum EngColorList : int
    {
        [Display(Name = "Black")]
        Black = 1,
        [Display(Name = "Silver")]
        Silver = 2,
        [Display(Name = "Gray")]
        Gray = 3,
        [Display(Name = "Pink")]
        Pink = 5,
        [Display(Name = "Rose")]
        Rose = 6,
        //[Display(Name = "Jaune")]
        //Jaune = 7,
        //[Display(Name = "Champagne")]
        //Champagne = 8,
        //[Display(Name = "Rose Gold")]
        //RoseGold = 9
    };

    public enum CapacityList : int
    {
        [Display(Name = "8Go")]
        Go8 = 1,
        [Display(Name = "16Go")]
        Go16 = 2,
        [Display(Name = "32Go")]
        Go32 = 3,
        [Display(Name = "64Go")]
        Go64 = 4,
        [Display(Name = "128Go")]
        Go128 = 5,
        [Display(Name = "256Go")]
        Go256 = 6,
    };

    public enum EngCapacityList : int
    {
        [Display(Name = "8GB")]
        GB8 = 1,
        [Display(Name = "16GB")]
        GB16 = 2,
        [Display(Name = "32GB")]
        GB32 = 3,
        [Display(Name = "64GB")]
        GB64 = 4,
        [Display(Name = "128GB")]
        GB128 = 5,
        [Display(Name = "256GB")]
        GB256 = 6,
    };
 

    public enum ConditionList : int
    {
        [Display(Name = "Légère rayures")]
        LégèreRayures = 1,
        [Display(Name = "Rayure profondes")]
        RayureProfondes = 2,
        [Display(Name = "Chocs")]
        Chocs = 3,
        [Display(Name = "Fissures")]
        Fissures = 4,
        [Display(Name = "Ecran cassé")]
        EcranCassé = 5,
        [Display(Name = "Commeneuf")]
        Commeneuf = 6
    };

    public enum EngConditionList : int
    {
        [Display(Name = "Light scratches")]
        Lightscratches = 1,
        [Display(Name = "Deep scratches")]
        Deepscratches = 2,
        [Display(Name = "Shocks")]
        Shocks = 3,
        [Display(Name = "Cracks")]
        Cracks = 4,
        [Display(Name = "Screen broken")]
        Screenbroken = 5,
        [Display(Name = "As new")]
        Asnew = 6
    };

    public enum EngBoughtAtList : int
    {
        [Display(Name = "Auchan")]
        Auchan = 1,
        [Display(Name = "Carrefour")]
        Carrefour = 2,
        [Display(Name = "Casino")]
        Casino = 3,
        [Display(Name = "Cora")]
        Cora = 4,
        [Display(Name = "Jumbo")]
        Jumbo = 5,
        [Display(Name = "Leclerc")]
        Leclerc = 7,
        [Display(Name = "Les Mousquetaires")]
        LesMousquetaires = 8,
        [Display(Name = "Systeme U")]
        SystemeU = 9,
        [Display(Name = "Autre/Other")]
        Autre_Other = 10
    };


    public enum EmailTypes:int
    {
        [Display(Name = "Reception's Confirmation in France")]
        ReceptionsConfirmationInFR = 1,
        [Display(Name = "Reparation in progress")]
        ReparationInProgress = 2,
        [Display(Name = "Reparation in France")]
        ReparationInFrance = 3,
        [Display(Name = "Shipped from France")]
        ShippedFromFrance = 4,
        [Display(Name = "Need to ship in China")]
        NeedToShipToChina = 5,
        [Display(Name = "Reparation in China")]
        ReparationInChina = 6,
        [Display(Name = "Shipped from China")]
        ShippedFromChina = 7,
        [Display(Name = "Quotation request")]
        QuotationRequest = 8,

    }
    public enum ExcelSheetName 
    {
        French,
        English,
    };
}
