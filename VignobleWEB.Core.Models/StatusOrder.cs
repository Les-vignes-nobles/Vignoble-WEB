using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VignobleWEB.Core.Models
{
    public class StatusOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static readonly StatusOrder EnCours = new StatusOrder() { Id = 1, Name = "En cours de validation" };
        public static readonly StatusOrder Validee = new StatusOrder() { Id = 2, Name = "Validée" };
        public static readonly StatusOrder PreparationCommande = new StatusOrder() { Id = 3, Name = "Préparation en cours" };
        public static readonly StatusOrder PreparationTerminee = new StatusOrder() { Id = 4, Name = "Préparation terminée" };
        public static readonly StatusOrder Livraison = new StatusOrder() { Id = 5, Name = "En cours de livraison" };
        public static readonly StatusOrder Livree = new StatusOrder() { Id = 6, Name = "Livrée" };
        public static readonly StatusOrder Annulee = new StatusOrder() { Id = 7, Name = "Annulée" };

        public static List<StatusOrder> getList()
        {
            List<StatusOrder> list = new();

            list.Add(EnCours);
            list.Add(Validee);
            list.Add(PreparationCommande);
            list.Add(PreparationTerminee);
            list.Add(Livraison);
            list.Add(Livree);
            list.Add(Annulee);

            return list;
        }
    }
}
