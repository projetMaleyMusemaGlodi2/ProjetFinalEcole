using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecole.Classe
{
    class ClassShowNotification
    {
        public static ClassShowNotification objet;
        ClassNotification notification = new ClassNotification();

        public static ClassShowNotification GetInstance()
        {
            if (objet == null)
            {
                objet = new ClassShowNotification();
            }
            return objet;
        }

        public void show(String type)
        {
            switch (type)
            {
                case "champs":
                    notification.Display("Erreur", "Veuillez completer tous les champs", "Error");
                    notification.showNotification("Erreur", "Erreur de Requetes", Environment.CurrentDirectory + @"\images\Error.ico");

                    break;
                case "Erreur":
                    notification.Display("Erreur", "Erreur de Requetes", "Error");
                    notification.showNotification("Erreur", "Erreur de Requetes", Environment.CurrentDirectory + @"\images\Error.ico");

                    break;
                case "Enreigistrement":
                    notification.Display("Enreigistrement", "Enreigistrement avec Succes", "Success");
                    notification.showNotification("Success", "Enreigistrement avec Succes", Environment.CurrentDirectory + @"\images\Ok.ico");
                    break;
                case "Modification":
                    notification.Display("Modification", "Modification reusssi avec Succes", "Success");
                    notification.showNotification("Modification", "Modification reusssi avec succes", Environment.CurrentDirectory + @"\images\Ok.ico");
                    break;
                case "delete":
                    notification.Display("Suppression", "Suppression effectuee avec Succes", "Success");
                    notification.showNotification("Suppression", "Suppression effectuee avec Succes", Environment.CurrentDirectory + @"\images\Ok.ico");
                    break;
                case "configok":
                    notification.Display("Configuration", "Configuration effectuee avec Succes", "Success");
                    notification.showNotification("Configuration", "Configuration effectuee avec Succes", Environment.CurrentDirectory + @"\images\Ok.ico");
                    break;
                case "configNo":
                    notification.Display("Configuration", "Configuration non effectue", "Error");
                    notification.showNotification("Configuration", "Configuration non effectue", Environment.CurrentDirectory + @"\images\Error.ico");
                    break;
                case "testok":
                    notification.Display("Test", "Test effectuee avec Succes", "Success");
                    notification.showNotification("Test", "Test effectuee avec Succes", Environment.CurrentDirectory + @"\images\Ok.ico");
                    break;
                case "testNo":
                    notification.showNotification("Test", "Test de connexion a echouee", Environment.CurrentDirectory + @"\images\Error.ico");
                    break;
                case "selection":
                    notification.Display("Erreur", "veiller selectionner un element", "Error");
                    notification.showNotification("Test", "Test de connexion a echouee", Environment.CurrentDirectory + @"\images\Error.ico");
                    break;
                case "bas":
                    notification.Display("Erreur", "La quantité selectionée est trop grande à la quanité disponible en stock", "Error");
                    notification.showNotification("Erreur", "La quantité selectionée est trop grande à la quanité disponible en stock", Environment.CurrentDirectory + @"\images\Error.ico");
                    break;
                case "limite":
                    notification.Display("Erreur", "La quantité selectionée est  à la quanité disponible en stock", "Error");
                    notification.showNotification("Erreur", "La quantité selectionée est trop grande à la quanité disponible en stock", Environment.CurrentDirectory + @"\images\Error.ico");
                    break;
                case "suppression":
                    notification.Display("Erreur", "Impossible de faire cette suppression car le quantité disponible en stock est inferieur a la quantite recquis ", "Error");
                    notification.showNotification("Erreur", "Impossible de faire cette suppression car le quantité disponible en stock est inferieur a la quantite recquis", Environment.CurrentDirectory + @"\images\Error.ico");
                    break;
                case "admin":
                    notification.Display("Erreur", "Impossible d'executer cette action ! contacter l'administrateur du system", "Error");
                    notification.showNotification("Erreur", "Impossible d'executer cette action ! contacter l'administrateur du system", Environment.CurrentDirectory + @"\images\Error.ico");
                    break;


            }
        }
    }
}
