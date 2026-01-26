using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.Collections.Generic; // Pour List<>
using ClientConvertisseurV1.Services; // Pour WSService   // Pour Devise
using Microsoft.UI.Xaml.Controls;

namespace ClientConvertisseurV1.Views
{
    public sealed partial class ConvertisseurEuroPage : Page
    {
        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            GetDataOnLoadAsync(); // Votre méthode de chargement des données (déjà faite précédemment)
        }

        // --- AJOUTEZ CETTE MÉTHODE POUR CORRIGER L'ERREUR ---
        private void BtnConvertir_Click(object sender, RoutedEventArgs e)
        {
            // 1. On vérifie qu'une devise est bien sélectionnée dans la ComboBox
            if (CbDevise.SelectedItem is Devise deviseSelectionnee)
            {
                // 2. On vérifie que le montant saisi est bien un chiffre valide
                if (double.TryParse(TxtMontantEuro.Text, out double montantEuro))
                {
                    // 3. Calcul : Montant * Taux
                    double resultat = montantEuro * deviseSelectionnee.Taux;

                    // 4. Affichage du résultat dans le TextBox en lecture seule
                    TxtResultat.Text = resultat.ToString();
                }
                else
                {
                    // Si ce n'est pas un chiffre (ex: du texte), on met 0 ou un message
                    TxtResultat.Text = "Montant invalide";
                }
            }
            else
            {
                // Gestion d'erreur si aucune devise n'est choisie (demandé page 11 du TP)
                TxtResultat.Text = "Sélectionnez une devise";
            }
        }
        // ----------------------------------------------------
        private async void GetDataOnLoadAsync()
        {
            // ATTENTION : Vérifiez bien le port ici (ex: 7243). 
            // Regardez votre fichier launchSettings.json ou l'URL quand vous lancez l'API.
            WSService service = new WSService("https://localhost:7243/api/");

            List<Devise> result = await service.GetDevisesAsync("devises");

            if (result == null)
            {
                // Si l'API ne répond pas, on affiche une erreur
                MessageAsync("API non disponible !", "Erreur");
            }
            else
            {
                // On remplit la ComboBox avec la liste des devises récupérées
                CbDevise.ItemsSource = result;
                // On indique quel champ de l'objet Devise afficher (le nom de la devise)
                CbDevise.DisplayMemberPath = "NomDevise";
            }
        }

        // 2. Méthode utilitaire pour afficher des boîtes de dialogue (Pop-up)
        private async void MessageAsync(string content, string title)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "Ok"
            };

            // Indispensable pour que la pop-up s'affiche par-dessus la fenêtre WinUI
            dialog.XamlRoot = this.Content.XamlRoot;

            await dialog.ShowAsync();
        }
        // ... Votre méthode GetDataOnLoadAsync est normalement ici ...
    }
}