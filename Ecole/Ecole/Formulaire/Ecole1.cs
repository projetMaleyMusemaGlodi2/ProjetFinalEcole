using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.IO;
using DevExpress.XtraReports.UI;
using System.Management;
using GsmComm.PduConverter;
using GsmComm.GsmCommunication;
using Ecole.Formulaire;
using Ecole.report;
using Ecole.usercontroles;
using Ecole.Classe;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;


namespace Ecole
{
    public partial class Ecole1 : DevExpress.XtraEditors.XtraForm
    {
        public Ecole1()
        {
            InitializeComponent();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(them, true, true);
        }

        clsMessagerie message = new clsMessagerie();
        int dep;
        eleve el1 = new eleve();
        parametre par1 = new parametre();
        cours cr1 = new cours();
        Enseignant ens = new Enseignant();
        horaire hor1 = new horaire();
        bibliotheque bib1 = new bibliotheque();
        OutgoingSmsPdu[] pdus;
        SmsSubmitPdu pdu;
        parametre param;

        OpenFileDialog ofdImage = new OpenFileDialog();

        public SqlConnection myconn = new SqlConnection();
        public SqlCommand mycomm = new SqlCommand();
        public SqlDataAdapter adpt1 = new SqlDataAdapter();
        connexion ap = new connexion();
        clsBase deb;
        ClIntelligence glo;

        int grame = DateTime.Now.Hour;
        int grame1 = DateTime.Now.Minute;
        int grame2 = DateTime.Now.Second;

        string Tempannee;
        string Tempclasse;
        string TempSection;
        string TempOption;

        string NomUser;
        //===========================================================================================

        private void chargerComboAdresse2(string nomTable, string ChampDesigne, System.Windows.Forms.ComboBox combo, string champ, string valeur)
        {
            glo = new ClIntelligence();
            deb = new clsBase();
            deb.NomTable1 = nomTable;
            deb.NomChamp1 = ChampDesigne;
            glo.loadCombo(deb, combo, champ, valeur);
        }

        void select_Adresse(string nomtable)
        {
            try
            {
                deb = new clsBase();
                deb.NomTable1 = nomtable;
                ClIntelligence.GetInstance().LoadDataGrid(deb.NomTable1, GridAdressage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de " + ex, "production d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void deleteAdressage(string nomTable, string champ)
        {
            deb = new clsBase();
            deb.Code1 = int.Parse(txtCodeAdress.Text.Trim());
            deb.NomTable1 = nomTable; deb.NomChamp1 = champ;
            ClIntelligence.GetInstance().deleteFrom(deb);
            MessageBox.Show("Suppréssion effectuée avec succès", "Message de confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            select_Adresse(nomTable);
        }


        void deleteAdress()
        {

            if (txtCodeAdress.Text == "")
            {
                MessageBox.Show("Selectionnez l'element dans la liste ci-dessous svp", "Selection Obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (radionation.Checked == true)
                {
                    deleteAdressage("pays", "codepays");
                }                
                else if (radioville.Checked == true)
                {
                    deleteAdressage("ville", "codeville");
                }
                else if (radiocom.Checked == true)
                {
                    deleteAdressage("commune", "codecommune");
                }
                else if (radioquartier.Checked == true)
                {
                    deleteAdressage("quartier", "codequartier");
                }
                else if (radioAvenue.Checked == true)
                {
                    deleteAdressage("tAvenue", "codeAvenue");
                }
                else
                {
                    MessageBox.Show("Selectionnez un Element svp", "Selection Obligatoire", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
            }


        }


        void testeFile() {            

            if ((Directory.Exists("C:\\MBC_soft") == true) && File.Exists("C:\\MBC_soft\\dbname.txt") == true && File.Exists("C:\\MBC_soft\\password.txt") == true && File.Exists("C:\\MBC_soft\\server.txt") == true && File.Exists("C:\\MBC_soft\\username.txt") == true)
            {  
                defaultLookAndFeel1.LookAndFeel.SkinName = "Blue";

                label_telephone.Visible = false;
                label_telephone_nom.Visible = false;
                label_telephone_postnom.Visible = false;
                radioprevision.Checked = true;
                cmbAnneeDebut.SelectedIndex = 0;

                timer1.Start();

                navframe.SelectedPage = acceuil;
                BaredeMenu.Enabled = false;
                //XtraMessageBox.Show(name.Text + pass.Text);
                radio_tous_bulletin.Checked = true;
                controlAcces();


                //par1.chargementanne(dataGridView9);
                load_message();

                par1.chargementclasse(gridparam1);
                par1.chargementcombo_section_designe(combo_section_inscription);
                par1.chargementcombo_section_designe(combo_section_affect);
                par1.chargementcombo_section_designe(combo_code_section_affect_horaire);



                param4.Enabled = false;
                param3.Enabled = false;
                

                //codepay3.Enabled = true;

                el1.chargementeleve(dataGridView1);
                el1.chargementcombocommune(combocommune);
                el1.chargementcomboquartier(comboquartier);
                el1.chargementcomboville(comboville);
                el1.chargementcombonation(combonation);
                el1.chargementcombocodeclasse2(comboclasse2);
                el1.chargementcombo_annee_designe(comboannee2);
                par1.chargementcombo_option_designe(combo_option_inscription);
                el1.chargementcombo_annee_designe(annee_affect);
                el1.chargementinscription(dataGridView2, txtcomboAnneeDebut.Text);
                          
                el1.chargementcombo_inscription(cmbElevePaie);               
                //ens.chargementcombocodecompte(comboutilisateur);
                el1.chargementcombotypefrais(cmbtypefrais);
                el1.chargementpaiement(gridpaiement,txtcomboAnneeDebut.Text);               
                cr1.chargementcours(gridcours);
                cr1.chargementperiode(gridperiode);                
                cr1.chargementcombocours(cours_affect);
                cr1.chargementcomboeleve(comboeleve);
                cr1.chargementcombo_periode_designe(comboperiode);
                cr1.chargementcotation(gridcotation, txtcomboAnneeDebut.Text);
                cr1.chargementcombo_enseignant_designe(enseignant_affect);
                cr1.chargementaffectation(gridaffectation, txtcomboAnneeDebut.Text);
                el1.chargementcombocodeclasse2(classe_affect);
                cr1.chargementcombo_periode_designe(combocode_periode_affect);
                par1.chargementcombo_option_designe(combocode_option_affect);

                               
                //cr1.chargement_code_eleve_cote(combo_ele);


                par1.chargementcombo_section_designe(cmbSectionBulletin);
                par1.chargementcombo_option_designe(cmbOptionBulletin);
                ClIntelligence.GetInstance().chargementcombo_classe_designe(cmbClasseBulletin);
                ClIntelligence.GetInstance().chargementcombo_annee_designe(cmbanneeBulletin);
                ClIntelligence.GetInstance().chargementcombo_annee_designe(cmbannee2Bulletin);
                ClIntelligence.GetInstance().chargementcombo_annee_designe(cmbAnneeDebut);


                el1.chargementcombocodeclasse2(combo_classe_affect_horaire);
                par1.chargementcombo_option_designe(combo_option_affect_horaire);
                el1.chargementcombo_annee_designe(combo_anne_affect_horaire);
                cr1.chargementcombo_enseignant_designe(combo_ens_affect_horaire);
                cr1.chargementcombocours(combo_cours_affect_horaire);
                
                //cr1.chargementcomboenseignant(combo_ens_gestionnaire);

                hor1.chargementcombo_jours_designe(comb_jours_affect_horaire);
                bib1.chargementcombocodelivre(combo_code_livre);
                hor1.chargement_horaire(gridhoraire,txtcomboAnneeDebut.Text);
                hor1.chargement_affectation_horaire(grid_affectation_horaire);

                //ens.chargementcombocodecomptable(combo_code_comptable_entree);
                //ens.chargementcombocodecomptable(combo_comptable_sortie);
                //ens.chargementcombo_utilisateur_login(name);

                ens.chargementEns(gridEnseignant);
                gridControl4.DataSource = ens.chargement_comptable();
                gridControl3.DataSource = ens.chargement_solde_caisse();
                gridControl1.DataSource = ens.chargement_ressource();



                gridControl10.DataSource = bib1.chargement_recherche_emprunt_livre(txtcomboAnneeDebut.Text);
                gridControl_recherche_inscription_emprunt.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
                gridControl5.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
                //gridControl_em.DataSource = bib1.chargement_emprunt();
                gridControl2.DataSource = ens.chargement_depense();
                gridControl7.DataSource = ens.chargement_utilisateur();
                gridControl6.DataSource = bib1.chargement_livre();
                gridControl9.DataSource = bib1.chargement_emprunt(txtcomboAnneeDebut.Text);
                gridControl11.DataSource = bib1.chargement_remise_livre(txtcomboAnneeDebut.Text);
                gridControl12.DataSource = cr1.chargement_proclamation();

                
                par1.chargementcombo_section_designe(combo_section_pourcent);
                par1.chargementcombo_ption_cours(combo_option_pourcent);
                par1.chargementcombo_classe_pourcent(combo_classe_pourcent);
                cr1.chargementcombo_periode_pourcent(combo_periode_pourcent);

                bib1.chargementcombocodelivre(cmbLivreRetour);

                gridControl8.DataSource = ClIntelligence.GetInstance().chargementCours();
                gridControl13.DataSource = ClIntelligence.GetInstance().chargementEnseignant();
                gridControl14.DataSource = ClIntelligence.GetInstance().chargementLivre();
                radio_humanite.Checked = true;

            }
            else
            {
                FormConfig frm = new FormConfig();
                frm.ShowDialog();

            }
        }

        //=================================================================================
        //CONTROLE DES ACCES

        private void getCodeAdresse(string nomTable, string champDesigne, string champCode)
        {
            glo = new ClIntelligence();
            deb = new clsBase();
            deb.NomTable1 = nomTable;
            deb.NomChamp1 = champCode;
            glo.getcode_Combo(deb, txtcomboRefAdresse, champDesigne, cmbRefAdresse.Text);

        }

        private void getCodeAdresseEleve(string nomTable, string champDesigne, string champCode,TextBox texte, string valeur)
        {
            glo = new ClIntelligence();
            deb = new clsBase();
            deb.NomTable1 = nomTable;
            deb.NomChamp1 = champCode;
            glo.getcode_Combo(deb, texte, champDesigne, valeur);

        }


        public void controlAcces()
        {
            LabelUser.Text = NomUser;
            if (fonction_login == "Comptable")
            {
                MenuStaInscription.Enabled = false;
                MenuStaReussite.Enabled = false;
                MenuRapport.Enabled = false;

                PagePresence.Enabled = false;
                MenuSaveEcole.Enabled = false;
                btnPageEnregistrerEleve.Enabled = false;
                btnPageInscriptEleve.Enabled = false;
                btnPageCours.Enabled = false;
                btnPagePeriode.Enabled = false;
                btnPageAffectationCours.Enabled = false;
                btnPageCotation.Enabled = false;
                btnPageLIvre.Enabled = false;
                btnPageUmprunt.Enabled = false;
                btnPageRemiseLivre.Enabled = false;
                btnPageJours.Enabled = false;
                btnPageAffectHoraire.Enabled = false;
                BtnPagePrintHoraire.Enabled = true;
                BtnPageBulletin.Enabled = false;
                BtnPagePalmaress.Enabled = false;
                btnpageEnvoieMessage.Enabled = false;
                btnPageReceptionMessage.Enabled = false;
                BtnPageEnregistrePersonnel.Enabled = false;
                BtnPageAffectPersonnelService.Enabled = false;

                btnPageParamBackup.Enabled = false;
                BtnPageParamDeconnexion.Enabled = false;
                btnPageParamEncours.Enabled = false;
                BtnPageParamInscription.Enabled = false;
                btnPageParamMisajour.Enabled = false;
                btnPageParamMiseAjours.Enabled = false;
                BtnPageParamRestauration.Enabled = false;
                btnPageParamUtilisateur.Enabled = false;

                btnPageHistoBibliotheque.Enabled = false;
                btnPageHistoCotation.Enabled = false;
                btnPageHistoPaiement.Enabled = false;
                BtnPageHistoriqueInscription.Enabled = false;

                btnPagePaiementEleve.Enabled = true;
                btnPageCaisse.Enabled = true;
                BtnpageListePrevision.Enabled = true;

                btnSavePaiement.Enabled = true;
                btnDeletePaiement.Enabled = false;

                btnSaveEntree.Enabled = true;
                btnDeleteEntree.Enabled = false;
                btnSaveDepense.Enabled = true;
                btnDeleteDepense.Enabled = false;

            }
            else if (fonction_login == "Bibliotheque")
            {
                MenuStaInscription.Enabled = false;
                MenuStaReussite.Enabled = false;
                MenuRapport.Enabled = false;

                PagePresence.Enabled = false;
                MenuSaveEcole.Enabled = false;
                btnPageEnregistrerEleve.Enabled = false;
                btnPageInscriptEleve.Enabled = false;

                btnPageCours.Enabled = false;
                btnPagePeriode.Enabled = false;
                btnPageAffectationCours.Enabled = false;
                btnPageCotation.Enabled = false;

                btnPageLIvre.Enabled = true;
                btnPageUmprunt.Enabled = true;
                btnPageRemiseLivre.Enabled = true;

                btnPageJours.Enabled = false;
                btnPageAffectHoraire.Enabled = false;
                BtnPagePrintHoraire.Enabled = true;

                BtnPageBulletin.Enabled = false;
                BtnPagePalmaress.Enabled = false;

                btnpageEnvoieMessage.Enabled = false;
                btnPageReceptionMessage.Enabled = false;

                BtnPageEnregistrePersonnel.Enabled = false;
                BtnPageAffectPersonnelService.Enabled = false;

                btnPageParamBackup.Enabled = false;
                BtnPageParamDeconnexion.Enabled = false;
                btnPageParamEncours.Enabled = false;
                BtnPageParamInscription.Enabled = false;
                btnPageParamMisajour.Enabled = false;
                btnPageParamMiseAjours.Enabled = false;
                BtnPageParamRestauration.Enabled = false;
                btnPageParamUtilisateur.Enabled = false;

                btnPageHistoBibliotheque.Enabled = false;
                btnPageHistoCotation.Enabled = false;
                btnPageHistoPaiement.Enabled = false;
                BtnPageHistoriqueInscription.Enabled = false;

                btnPagePaiementEleve.Enabled = false;
                btnPageCaisse.Enabled = false;
                BtnpageListePrevision.Enabled = false;

                btnSaveAffectHoraire.Enabled = true;
                btnDeleteAffectHoraire.Enabled = false;
                btnUpdateAffectHoraire.Enabled = true;

                btnSaveJours.Enabled = false; ;
                btnUpdateJours.Enabled = false;
                btndeleteJours.Enabled = false;

                btnSaveLivre.Enabled = true;
                btnSaveUmpruntLivre.Enabled = true;
                btnSaveRemiseLivre.Enabled = true;

                btnDeleteLivre.Enabled = false;
                btnDeleteUmpruntLivre.Enabled = false;
                btnDeleteRemiseLivre.Enabled = false;



            }
            else if (fonction_login == "Titulaire")
            {
                MenuStaInscription.Enabled = false;
                MenuStaReussite.Enabled = false;
                MenuRapport.Enabled = false;

                PagePresence.Enabled = false;
                MenuSaveEcole.Enabled = false;
                btnPageEnregistrerEleve.Enabled = false;
                btnPageInscriptEleve.Enabled = false;

                btnPageCours.Enabled = false;
                btnPagePeriode.Enabled = false;
                btnPageAffectationCours.Enabled = false;
                btnPageCotation.Enabled = true;

                btnPageLIvre.Enabled = false;
                btnPageUmprunt.Enabled = false;
                btnPageRemiseLivre.Enabled = false;

                btnPageJours.Enabled = false;
                btnPageAffectHoraire.Enabled = false;
                BtnPagePrintHoraire.Enabled = true;

                BtnPageBulletin.Enabled = true;
                BtnPagePalmaress.Enabled = false;

                btnpageEnvoieMessage.Enabled = false;
                btnPageReceptionMessage.Enabled = false;

                BtnPageEnregistrePersonnel.Enabled = false;
                BtnPageAffectPersonnelService.Enabled = false;

                btnPageParamBackup.Enabled = false;
                BtnPageParamDeconnexion.Enabled = false;
                btnPageParamEncours.Enabled = false;
                BtnPageParamInscription.Enabled = false;
                btnPageParamMisajour.Enabled = false;
                btnPageParamMiseAjours.Enabled = false;
                BtnPageParamRestauration.Enabled = false;
                btnPageParamUtilisateur.Enabled = false;

                btnPageHistoBibliotheque.Enabled = false;
                btnPageHistoCotation.Enabled = false;
                btnPageHistoPaiement.Enabled = false;
                BtnPageHistoriqueInscription.Enabled = false;

                btnPagePaiementEleve.Enabled = false;
                btnPageCaisse.Enabled = false;
                BtnpageListePrevision.Enabled = false;

                btnDeleteAffectCours.Enabled = false;
                BtnDeleteCotation.Enabled = false;
                btnDeleteCours.Enabled = false;
                btnDeleteperiode.Enabled = false;

                btnUpdateCotation.Enabled = false;
                btnUpdateCours.Enabled = false;
                btnupdateperiode.Enabled = false;

                btnSaveAffectHoraire.Enabled = false;
                btnDeleteAffectHoraire.Enabled = false;
                btnUpdateAffectHoraire.Enabled = false;

            }

            else if (fonction_login== "DirecteurEtude") {

                MenuStaInscription.Enabled = true;
                MenuStaReussite.Enabled = true;
                MenuRapport.Enabled = true;

                PagePresence.Enabled = true;
                MenuSaveEcole.Enabled = false;
                btnPageEnregistrerEleve.Enabled = true;
                btnPageInscriptEleve.Enabled = true;

                btnPageCours.Enabled = true;
                btnPagePeriode.Enabled = true;
                btnPageAffectationCours.Enabled = true;
                btnPageCotation.Enabled = true;

                btnPageLIvre.Enabled = true;
                btnPageUmprunt.Enabled = true;
                btnPageRemiseLivre.Enabled = true;

                btnPageJours.Enabled = false;
                btnPageAffectHoraire.Enabled = true;                
                BtnPagePrintHoraire.Enabled = true;

                BtnPageBulletin.Enabled = true;
                BtnPagePalmaress.Enabled = true;

                btnpageEnvoieMessage.Enabled = true;
                btnPageReceptionMessage.Enabled = true;

                BtnPageEnregistrePersonnel.Enabled = true;
                BtnPageAffectPersonnelService.Enabled = true;

                btnPageParamBackup.Enabled = false;
                BtnPageParamDeconnexion.Enabled = false;
                btnPageParamEncours.Enabled = false;
                BtnPageParamInscription.Enabled = false;
                btnPageParamMisajour.Enabled = false;
                btnPageParamMiseAjours.Enabled = false;
                BtnPageParamRestauration.Enabled = false;
                btnPageParamUtilisateur.Enabled = false;

                btnPageHistoBibliotheque.Enabled = false;
                btnPageHistoCotation.Enabled = false;
                btnPageHistoPaiement.Enabled = false;
                BtnPageHistoriqueInscription.Enabled = false;

                btnPagePaiementEleve.Enabled = true;
                btnPageCaisse.Enabled = true;
                BtnpageListePrevision.Enabled = true;               


                btnNewEleve.Enabled = true;
                btnNewInscription.Enabled = true;

                btnSaveAffectCours.Enabled = true;
                btnSaveCotation.Enabled = true;
                BtnSaveCours.Enabled = true;
                btnSavePeriode.Enabled = true;

                btnSaveEleve.Enabled = true;
                btnSaveInscription.Enabled = true;

                btnDeleteEleve.Enabled = false;
                btnDeleteInscription.Enabled = false;

                btnDeleteAffectCours.Enabled = false;
                BtnDeleteCotation.Enabled = false;
                btnDeleteCours.Enabled = false;
                btnDeleteperiode.Enabled = false;

                btnUpdateCotation.Enabled = false;
                btnUpdateCours.Enabled = false;
                btnupdateperiode.Enabled = false;


                btnSaveAffectHoraire.Enabled = false;
                btnDeleteAffectHoraire.Enabled = false;
                btnUpdateAffectHoraire.Enabled = false;

                btnSavePaiement.Enabled = false;
                btnDeletePaiement.Enabled = false;

                btnSaveEntree.Enabled = false;
                btnDeleteEntree.Enabled = false;
                btnSaveDepense.Enabled = false;
                btnDeleteDepense.Enabled = false;

                btnSaveLivre.Enabled = false;
                btnSaveUmpruntLivre.Enabled = false;
                btnSaveRemiseLivre.Enabled = false;
                btnDeleteLivre.Enabled = false;
                btnDeleteUmpruntLivre.Enabled = false;
                btnDeleteRemiseLivre.Enabled = false;

            }

            else if (fonction_login == "Prefet")
            {
                MenuStaInscription.Enabled = true;
                MenuStaReussite.Enabled = true;
                MenuRapport.Enabled = true;

                MenuSaveEcole.Enabled = false;
                btnPageEnregistrerEleve.Enabled = true;
                btnPageInscriptEleve.Enabled = true;

                btnPageCours.Enabled = true;
                btnPagePeriode.Enabled = true;
                btnPageAffectationCours.Enabled = true;
                btnPageCotation.Enabled = true;

                btnPageLIvre.Enabled = true;
                btnPageUmprunt.Enabled = true;
                btnPageRemiseLivre.Enabled = true;

                btnPageJours.Enabled = false;
                btnPageAffectHoraire.Enabled = false;
                BtnPagePrintHoraire.Enabled = true;

                BtnPageBulletin.Enabled = true;
                BtnPagePalmaress.Enabled = true;

                btnpageEnvoieMessage.Enabled = true;
                btnPageReceptionMessage.Enabled = true;

                BtnPageEnregistrePersonnel.Enabled = true;
                BtnPageAffectPersonnelService.Enabled = true;

                btnPageParamBackup.Enabled = false;
                BtnPageParamDeconnexion.Enabled = false;
                btnPageParamEncours.Enabled = false;
                BtnPageParamInscription.Enabled = false;
                btnPageParamMisajour.Enabled = false;
                btnPageParamMiseAjours.Enabled = false;
                BtnPageParamRestauration.Enabled = false;
                btnPageParamUtilisateur.Enabled = false;

                btnPageHistoBibliotheque.Enabled = false;
                btnPageHistoCotation.Enabled = false;
                btnPageHistoPaiement.Enabled = false;
                BtnPageHistoriqueInscription.Enabled = false;

                btnPagePaiementEleve.Enabled = true;
                btnPageCaisse.Enabled = true;
                BtnpageListePrevision.Enabled = true;


                btnNewEleve.Enabled = true;
                btnNewInscription.Enabled = true;

                btnSaveAffectCours.Enabled = true;
                btnSaveCotation.Enabled = false;
                BtnSaveCours.Enabled = true;
                btnSavePeriode.Enabled = true;

                btnSaveEleve.Enabled = true;
                btnSaveInscription.Enabled = true;

                btnDeleteEleve.Enabled = false;
                btnDeleteInscription.Enabled = false;

                btnDeleteAffectCours.Enabled = false;
                BtnDeleteCotation.Enabled = false;
                btnDeleteCours.Enabled = false;
                btnDeleteperiode.Enabled = false;

                btnUpdateCotation.Enabled = false;
                btnUpdateCours.Enabled = false;
                btnupdateperiode.Enabled = false;

                btnSaveAffectHoraire.Enabled = false;
                btnDeleteAffectHoraire.Enabled = false;
                btnUpdateAffectHoraire.Enabled = false;

                btnSavePaiement.Enabled = false;
                btnDeletePaiement.Enabled = false;

                btnSaveEntree.Enabled = false;
                btnDeleteEntree.Enabled = false;
                btnSaveDepense.Enabled = false;
                btnDeleteDepense.Enabled = false;

                btnSaveLivre.Enabled = false;
                btnSaveUmpruntLivre.Enabled = false;
                btnSaveRemiseLivre.Enabled = false;
                btnDeleteLivre.Enabled = false;
                btnDeleteUmpruntLivre.Enabled = false;
                btnDeleteRemiseLivre.Enabled = false;

            }

            else if (fonction_login == "Secretaire")
            {
                MenuStaInscription.Enabled = true;
                MenuStaReussite.Enabled = true;
                MenuRapport.Enabled = true;

                PagePresence.Enabled = false;
                MenuSaveEcole.Enabled = false;
                btnPageEnregistrerEleve.Enabled = true;
                btnPageInscriptEleve.Enabled = true;

                btnPageCours.Enabled = true;
                btnPagePeriode.Enabled = false;
                btnPageAffectationCours.Enabled = false;
                btnPageCotation.Enabled = false;

                btnPageLIvre.Enabled = false;
                btnPageUmprunt.Enabled = false;
                btnPageRemiseLivre.Enabled = false;

                btnPageJours.Enabled = false;
                btnPageAffectHoraire.Enabled = false;
                BtnPagePrintHoraire.Enabled = true;

                BtnPageBulletin.Enabled = true;
                BtnPagePalmaress.Enabled = true;

                btnpageEnvoieMessage.Enabled = true;
                btnPageReceptionMessage.Enabled = true;

                BtnPageEnregistrePersonnel.Enabled = false;
                BtnPageAffectPersonnelService.Enabled = false;

                btnPageParamBackup.Enabled = false;
                BtnPageParamDeconnexion.Enabled = false;
                btnPageParamEncours.Enabled = false;
                BtnPageParamInscription.Enabled = false;
                btnPageParamMisajour.Enabled = false;
                btnPageParamMiseAjours.Enabled = false;
                BtnPageParamRestauration.Enabled = false;
                btnPageParamUtilisateur.Enabled = false;

                btnPageHistoBibliotheque.Enabled = false;
                btnPageHistoCotation.Enabled = false;
                btnPageHistoPaiement.Enabled = false;
                BtnPageHistoriqueInscription.Enabled = false;

                btnPagePaiementEleve.Enabled = false;
                btnPageCaisse.Enabled = false;
                BtnpageListePrevision.Enabled = false;

                btnNewEleve.Enabled = true;
                btnNewInscription.Enabled = true;

                btnSaveEleve.Enabled = false;
                btnSaveInscription.Enabled = false;

                btnDeleteEleve.Enabled = false;
                btnDeleteInscription.Enabled = false;               

                btnDeleteAffectCours.Enabled = false;
                BtnDeleteCotation.Enabled = false;
                btnDeleteCours.Enabled = false;
                btnDeleteperiode.Enabled = false;

                btnUpdateCotation.Enabled = false;
                btnUpdateCours.Enabled = false;
                btnupdateperiode.Enabled = false;

                btnSaveAffectHoraire.Enabled = false;
                btnDeleteAffectHoraire.Enabled = false;
                btnUpdateAffectHoraire.Enabled = false;

                btnSavePaiement.Enabled = false;
                btnDeletePaiement.Enabled = false;

            }

            else if (fonction_login == "Enseignant")
            {
                MenuStaInscription.Enabled = false;
                MenuStaReussite.Enabled = false;
                MenuRapport.Enabled = false;

                MenuSaveEcole.Enabled = false;
                btnPageEnregistrerEleve.Enabled = false;
                btnPageInscriptEleve.Enabled = false;

                btnPageCours.Enabled = true;
                btnPagePeriode.Enabled = true;
                btnPageAffectationCours.Enabled = true;
                btnPageCotation.Enabled = false;

                btnPageLIvre.Enabled = true;
                btnPageUmprunt.Enabled = true;
                btnPageRemiseLivre.Enabled = true;

                btnPageJours.Enabled = false;
                btnPageAffectHoraire.Enabled = false;
                BtnPagePrintHoraire.Enabled = true;

                BtnPageBulletin.Enabled = false;
                BtnPagePalmaress.Enabled = false;

                btnpageEnvoieMessage.Enabled = false;
                btnPageReceptionMessage.Enabled = false;

                BtnPageEnregistrePersonnel.Enabled = false;
                BtnPageAffectPersonnelService.Enabled = false;

                btnPageParamBackup.Enabled = false;
                BtnPageParamDeconnexion.Enabled = false;
                btnPageParamEncours.Enabled = false;
                BtnPageParamInscription.Enabled = false;
                btnPageParamMisajour.Enabled = false;
                btnPageParamMiseAjours.Enabled = false;
                BtnPageParamRestauration.Enabled = false;
                btnPageParamUtilisateur.Enabled = false;

                btnPageHistoBibliotheque.Enabled = false;
                btnPageHistoCotation.Enabled = false;
                btnPageHistoPaiement.Enabled = false;
                BtnPageHistoriqueInscription.Enabled = false;

                btnPagePaiementEleve.Enabled = false;
                btnPageCaisse.Enabled = false;
                BtnpageListePrevision.Enabled = false;

                btnNewEleve.Enabled = false;
                btnNewInscription.Enabled = false;

                btnSaveEleve.Enabled = false;
                btnSaveInscription.Enabled = false;

                btnDeleteEleve.Enabled = false;
                btnDeleteInscription.Enabled = false;

                btnDeleteAffectCours.Enabled = false;
                BtnDeleteCotation.Enabled = false;
                btnDeleteCours.Enabled = false;
                btnDeleteperiode.Enabled = false;

                BtnSaveCours.Enabled = false;
                btnSavePeriode.Enabled = false;
                btnSaveCotation.Enabled = false;
                btnSaveAffectCours.Enabled = false;

                btnUpdateCotation.Enabled = false;
                btnUpdateCours.Enabled = false;
                btnupdateperiode.Enabled = false;

                btnSaveAffectHoraire.Enabled = false;
                btnDeleteAffectHoraire.Enabled = false;
                btnUpdateAffectHoraire.Enabled = false;

                btnSavePaiement.Enabled = false;
                btnDeletePaiement.Enabled = false;

                btnSaveLivre.Enabled = false;
                btnSaveUmpruntLivre.Enabled = false;
                btnSaveRemiseLivre.Enabled = false;

                btnDeleteLivre.Enabled = false;
                btnDeleteUmpruntLivre.Enabled = false;
                btnDeleteRemiseLivre.Enabled = false;
                btnPrintCarteBibliotheque.Enabled = false;

            }

            else if (fonction_login == "Directeur")
            {

            }

        }


        public bool IsNumeric(string Nombre)
        {
            bool istrue = false; int k = 0;
            try
            {
                for (int i = 0; i < Nombre.ToString().Length; i++)
                {
                    if (!char.IsNumber(Nombre.ToString(), i))
                    {
                        k += 1;
                    }
                }
                if (k == 0) istrue = true;
            }
            catch (Exception)
            { }
            return istrue;
        }



        //===========================================================================================
        //MESSAGERIE
        private delegate void SetTextCallback(string text);
        private int port;
        private int baudRate;
        private int timeout;
        private GsmCommMain comm;
        private DataTable dt = new DataTable();

        public void SetData(int port, int baudRate, int timeout)
        {
            this.port = port;
            this.baudRate = baudRate;
            this.timeout = timeout;
        }

        public void GetData(out int port, out int baudRate, out int timeout)
        {
            port = this.port;
            baudRate = this.baudRate;
            timeout = this.timeout;
        }

        private bool EnterNewSettings()
        {
            int newPort;
            int newBaudRate;
            int newTimeout;

            try
            {
                newPort = int.Parse(portnumber.Text);
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Invalid port number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboPort.Focus();
                return false;
            }

            try
            {
                newBaudRate = int.Parse(cboBaudRate.Text);
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Invalid baud rate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboBaudRate.Focus();
                return false;
            }

            try
            {
                newTimeout = int.Parse(cboTimeout.Text);
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Invalid timeout value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboTimeout.Focus();
                return false;
            }

            SetData(newPort, newBaudRate, newTimeout);

            return true;
        }



        private void Output(string text)
        {

            try
            {
                txtOutput.AppendText(text);
                Output1.AppendText(text);
            }

            catch (Exception)
            {
                MessageBox.Show("Message envoie");

            }

        }

        void envoie_message()
        {

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                // Send an SMS message
                SmsSubmitPdu pdu;
                bool alert = chkAlert.Checked;
                bool unicode = chkUnicode.Checked;

                if (!alert && !unicode)
                {
                    // The straightforward version
                    pdu = new SmsSubmitPdu(MStexte.Text, Numtext.Text, "");  // "" indicate SMSC No
                }
                else
                {
                    // The extended version with dcs
                    byte dcs;
                    if (!alert && unicode)
                        dcs = DataCodingScheme.NoClass_16Bit;
                    else if (alert && !unicode)
                        dcs = DataCodingScheme.Class0_7Bit;
                    else if (alert && unicode)
                        dcs = DataCodingScheme.Class0_16Bit;
                    else
                        dcs = DataCodingScheme.NoClass_7Bit; // should never occur here

                    pdu = new SmsSubmitPdu(MStexte.Text, Numtext.Text, "", dcs);
                }

                // Send the same message multiple times if this is set
                int times = chkMultipleTimes.Checked ? int.Parse(txtSendTimes.Text) : 1;

                if (!comm.IsOpen())
                    comm.Open();
                // Send the message the specified number of times
                for (int i = 0; i < times; i++)
                {
                    comm.SendMessage(pdu);
                    Output(string.Format("Message {0} of {1} sent.\n", i + 1, times));
                    Output("");
                }

                MessageBox.Show("Message envoyé avec succès", "Confirmation sending", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

            Cursor.Current = Cursors.Default;

        }





        public string GetAllPorts(System.Windows.Forms.ComboBox combo)
        {
            //string MODEMS = "";
            string modems = "";

            try
            {

                //combo.Items.Clear();

                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_POTSModem ");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if ((string)queryObj["Status"] == "OK")
                    {

                        combo.Items.Add(queryObj["AttachedTo"] + " - " + System.Convert.ToString(queryObj["Description"]));
                    }
                    if (combo.Items.Count > 0)
                    {
                        combo.SelectedIndex = 0;
                    }
                }

                return modems;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de la requette", "Erreur de" + ex.Message);
                return "";
            }

        }

        void load_message()
        {

            GetAllPorts(cboPort);

            cboPort.Items.Add("19");
            //cboPort.Items.Add("2");
            //cboPort.Items.Add("3");
            //cboPort.Items.Add("4");
            cboPort.Text = port.ToString();

            //deplacer_message_read(dep);
            cboBaudRate.Visible = false;
            cboTimeout.Visible = false;
            
            chkMultipleTimes.Checked = true;
            chkUnicode.Checked = true;
            rbMessageSIM.Checked = true;


            //btndeconnect.Enabled = false;

            dt.Columns.Add("Sender", typeof(string));
            dt.Columns.Add("Time", typeof(string));
            dt.Columns.Add("Message", typeof(string));



        }


        void envoiePlusieurGrandMessage(GridView grid,string champ)
        {
            try
            {
                if (MessageBox.Show("Voulez-vous confirmer l'envoi.?", "ENVOI A TOUS ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                    if (gridView_parent.RowCount > 0)
                    {

                        SmsSubmitPdu pdu;

                        for (int x = 0; x < gridView_parent.RowCount; x++)
                        {
                            string num = grid.GetRowCellValue(x, champ).ToString();

                            ClsMessagerieInsert m = new ClsMessagerieInsert();
                            m.Numero1 = num;
                            m.MessateTexte1 = MStexte.Text;
                            m.EtatSms1 = 0;
                            m.DateEvoie1 = DateTime.Now;
                            m.Utilisateur1 = LabelUser.Text;
                            ClIntelligence.GetInstance().insertMessagerie(m);

                            Cursor.Current = Cursors.WaitCursor;
                            try
                            {
                                bool alert = chkAlert.Checked;
                                bool unicode = chkUnicode.Checked;

                                if (!alert && !unicode)
                                {

                                }
                                else
                                {

                                    string message = MStexte.Text + "--Institut Zanner--";
                                    if (message.Length > 160)
                                    {

                                        double t = message.Length / 140;
                                        double f = Math.Round(t);
                                        int k = int.Parse(f.ToString()) + 1;
                                        pdus = new OutgoingSmsPdu[k];
                                        string ps = message.Substring(0, 140);
                                        int dep = 0;

                                        for (int i = 0; i < k; i++)
                                        {
                                            pdu = new SmsSubmitPdu(ps, Numtext.Text);
                                            pdus[i] = pdu;                                           

                                            dep = dep + ps.Length;

                                            if ((message.Length - dep) <= 140 && (message.Length - dep) > 2)
                                            {
                                                ps = message.Substring(ps.Length, message.Length - 1 - dep);
                                            }
                                            else if ((message.Length - dep) >= 139)
                                            {
                                                ps = message.Substring(dep, 140);
                                            }
                                        }
                                        if (!pubCon.comm.IsOpen()) pubCon.comm.Open();
                                        pubCon.comm.SendMessages(pdus);
                                        MessageBox.Show("Message envoye avec succes !!!!1");
                                    }
                                }
                                // Send the same message multiple times if this is set
                            }

                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.Message);
                            }
                            Cursor.Current = Cursors.Default;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Aucun personnel trouvé", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        void envoiePlusieurPetitMessage(GridView grid,string champ)
        {
            try
            {
                //bool envoie = false;
                if (MessageBox.Show("Voulez-vous confirmer l'envoi.?", "ENVOI A TOUS ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                    if (grid.RowCount > 0)
                    {

                        SmsSubmitPdu pdu;

                        for (int x = 0; x < grid.RowCount; x++)
                        {
                            string num = grid.GetRowCellValue(x, champ).ToString();


                            //if (!pubCon.comm.IsConnected()) { }


                            ClsMessagerieInsert m = new ClsMessagerieInsert();
                            m.Numero1 = num;
                            m.MessateTexte1 = MStexte.Text;
                            m.EtatSms1 = 0;
                            m.DateEvoie1 = DateTime.Now;
                            m.Utilisateur1 = LabelUser.Text;
                            ClIntelligence.GetInstance().insertMessagerie(m);

                            Cursor.Current = Cursors.WaitCursor;
                            try
                            {
                                // Send an SMS message                                
                                bool alert = chkAlert.Checked;
                                bool unicode = chkUnicode.Checked;

                                if (!alert && !unicode)
                                {
                                    // The straightforward version
                                    pdu = new SmsSubmitPdu(MStexte.Text, num, "");  // "" indicate SMSC No
                                }
                                else
                                {
                                    // The extended version with dcs
                                    byte dcs;
                                    if (!alert && unicode)
                                        dcs = DataCodingScheme.NoClass_16Bit;
                                    else if (alert && !unicode)
                                        dcs = DataCodingScheme.Class0_7Bit;
                                    else if (alert && unicode)
                                        dcs = DataCodingScheme.Class0_16Bit;
                                    else
                                        dcs = DataCodingScheme.NoClass_7Bit; // should never occur here

                                    pdu = new SmsSubmitPdu(MStexte.Text,num, "", dcs);
                                   
                                }

                                // Send the same message multiple times if this is set
                                int times = chkMultipleTimes.Checked ? int.Parse(txtSendTimes.Text) : 1;

                                if (!pubCon.comm.IsOpen())
                                    pubCon.comm.Open();
                                // Send the message the specified number of times
                                for (int i = 0; i < times; i++)
                                {
                                    pubCon.comm.SendMessage(pdu);
                                    Output(string.Format("Message {0} of {1} sent.\n", i + 1, times));
                                    Output("");
                                }

                                MessageBox.Show("Message envoyé avec succès", "Confirmation sending", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                //envoie = true;
                            }

                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.Message);
                            }

                            Cursor.Current = Cursors.Default;
                            //return envoie;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Aucun personnel trouvé", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

            bool envoie_une_personne() {


            bool envoie = false;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                ClsMessagerieInsert m = new ClsMessagerieInsert();
                m.Numero1 = Numtext.Text;
                m.MessateTexte1 = MStexte.Text;
                m.EtatSms1 = 0;
                m.DateEvoie1 = DateTime.Now;
                m.Utilisateur1 = LabelUser.Text;
                ClIntelligence.GetInstance().insertMessagerie(m);

                bool alert = chkAlert.Checked;
                bool unicode = chkUnicode.Checked;

                if (!alert && !unicode)
                {

                }
                else
                {

                    string message = MStexte.Text+"                        ";
                    if (message.Length > 160)
                    {
                        double t = message.Length / 140;
                        double f = Math.Round(t);
                        int k = int.Parse(f.ToString()) + 1;
                        pdus = new OutgoingSmsPdu[k];
                        string ps = message.Substring(0, 140);
                        int dep = 0;


                        for (int i = 0; i < k; i++)
                        {
                            pdu = new SmsSubmitPdu(ps, Numtext.Text);
                            

                            pdus[i] = pdu;
                            dep = dep + ps.Length;

                            if ((message.Length - dep) <= 140 && (message.Length - dep) > 2)
                            {
                                ps = message.Substring(ps.Length, message.Length - 1 - dep);
                            }
                            else if ((message.Length - dep) >= 139)
                            {
                                ps = message.Substring(dep, 140);
                            }
                        }
                        if (!pubCon.comm.IsOpen()) pubCon.comm.Open();
                        pubCon.comm.SendMessages(pdus);
                        MessageBox.Show("Message envoye avec succes !!!!1");
                        envoie = true;
                    }
                }
                // Send the same message multiple times if this is set
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                envoie = false;
            }
            Cursor.Current = Cursors.Default;
            return envoie;
        }




        
        //===================================================================================================================
        //LIRE MESSAGE
        private string GetMessageStorage()
        {
            string storage = string.Empty;
            if (rbMessageSIM.Checked)
                storage = PhoneStorageType.Sim;
            if (rbMessagePhone.Checked)
                storage = PhoneStorageType.Phone;
            if (storage.Length == 0)
                throw new ApplicationException("Unknown message storage.");
            else
                return storage;
        }


        private void BindGrid(SmsPdu pdu)
        {

            DataRow dr = dt.NewRow();
            SmsDeliverPdu data = (SmsDeliverPdu)pdu;

            dr[0] = data.OriginatingAddress.ToString();
            dr[1] = data.SCTimestamp.ToString();
            dr[2] = data.UserDataText;
            dt.Rows.Add(dr);

            grid_message2.DataSource = dt;
            

        }


        private string StatusToString(PhoneMessageStatus status)
        {
            // Map a message status to a string
            string ret;
            switch (status)
            {
                case PhoneMessageStatus.All:
                    ret = "All";
                    break;
                case PhoneMessageStatus.ReceivedRead:
                    ret = "Read";
                    break;
                case PhoneMessageStatus.ReceivedUnread:
                    ret = "Unread";
                    break;
                case PhoneMessageStatus.StoredSent:
                    ret = "Sent";
                    break;
                case PhoneMessageStatus.StoredUnsent:
                    ret = "Unsent";
                    break;
                default:
                    ret = "Unknown (" + status.ToString() + ")";
                    break;
            }
            return ret;
        }



        private void ShowMessage(SmsPdu pdu)
        {
            if (pdu is SmsSubmitPdu)
            {
                // Stored (sent/unsent) message
                SmsSubmitPdu data = (SmsSubmitPdu)pdu;
                Output("SENT/UNSENT MESSAGE");
                Output("Recipient: " + data.DestinationAddress);
                Output("Message text: " + data.UserDataText);
                Output("-------------------------------------------------------------------");
                return;
            }
            if (pdu is SmsDeliverPdu)
            {
                // Received message
                SmsDeliverPdu data = (SmsDeliverPdu)pdu;
                Output("RECEIVED MESSAGE");
                Output("Sender: " + data.OriginatingAddress);
                Output("Sent: " + data.SCTimestamp.ToString());
                Output("Message text: " + data.UserDataText);
                Output("-------------------------------------------------------------------");

                BindGrid(pdu);

                return;
            }
            if (pdu is SmsStatusReportPdu)
            {
                // Status report
                SmsStatusReportPdu data = (SmsStatusReportPdu)pdu;
                Output("STATUS REPORT");
                Output("Recipient: " + data.RecipientAddress);
                Output("Status: " + data.Status.ToString());
                Output("Timestamp: " + data.DischargeTime.ToString());
                Output("Message ref: " + data.MessageReference.ToString());
                Output("-------------------------------------------------------------------");
                return;
            }
            Output("Unknown message type: " + pdu.GetType().ToString());
        }


        void read_message()
        {

            Cursor.Current = Cursors.WaitCursor;
            string storage = GetMessageStorage();

            try
            {
                // Read all SMS messages from the storage

                DecodedShortMessage[] messages = comm.ReadMessages(PhoneMessageStatus.All, storage);
                foreach (DecodedShortMessage message in messages)
                {
                    Output(string.Format("Message status = {0}, Location = {1}/{2}",
                        StatusToString(message.Status), message.Storage, message.Index));
                    ShowMessage(message.Data);
                    Output("");
                }
                MessageBox.Show("Message read !!!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Cursor.Current = Cursors.Default;



        }



        void liere_message()
        {
            try
            {


                Cursor.Current = Cursors.WaitCursor;
                string storage = GetMessageStorage();

                try
                {
                    if (!comm.IsOpen())
                        comm.Open();
                    // Read all SMS messages from the storage

                    DecodedShortMessage[] messages = comm.ReadMessages(PhoneMessageStatus.All, storage);
                    foreach (DecodedShortMessage message in messages)
                    {
                        Output(string.Format("Message status = {0}, Location = {1}/{2}",
                            StatusToString(message.Status), message.Storage, message.Index));
                        ShowMessage(message.Data);
                        Output("");
                    }
                    MessageBox.Show("Message read !!!!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                Cursor.Current = Cursors.Default;




            }


            catch 
            {
                MessageBox.Show("Choisissez une option svp !!! soit SIM ou PHONE ","Choix Obligatoire",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
            }



        }

        private void deplacer_message_read(int dep)
        {
            try
            {
                if (dep == 0)
                {
                    btnsuivantM.Enabled = true;
                    btnprecedentM.Enabled = false;
                }
                else if (dep == grid_message2.RowCount - 2)
                {
                    btnsuivantM.Enabled = false;
                    btnprecedentM.Enabled = true;
                }
                else if (dep > 0 & dep < grid_message2.RowCount - 2)
                {
                    btnsuivantM.Enabled = true;
                    btnprecedentM.Enabled = true;
                }

                else
                {
                    btnsuivantM.Enabled = false;
                    btnprecedentM.Enabled = false;
                }

               Output1.Text = grid_message2[0, dep].Value.ToString() + "___" + grid_message2[1, dep].Value.ToString().Substring(0, 16) + "___" + grid_message2[2, dep].Value.ToString();


            }
            catch
            {

            }

        }



        //===========================================================================================





        

        private void deplacerclient(int dep)
        {


            try
            {
                if (dep == 0)
                {
                    btnsuivant.Enabled = true;
                    btnprecedent.Enabled = false;
                }
                else if (dep == dataGridView1.RowCount - 2)
                {
                    btnsuivant.Enabled = false;
                    btnprecedent.Enabled = true;
                }
                else if (dep > 0 & dep < dataGridView1.RowCount - 2)
                {
                    btnsuivant.Enabled = true;
                    btnprecedent.Enabled = true;
                }

                else
                {
                    btnsuivant.Enabled = false;
                    btnprecedent.Enabled = false;
                }

                //string nomat = nom.Text;
                //string postmat = post.Text;
                //string datemat = datenaiss.Text;

                //string premChar = nomat.Substring(0, 3);
                //string deuxChar = postmat.Substring(0, 3);
                //string troisChar = datemat.Substring(0, 2);

                //string matricule = dataGridView1[0, dep].Value.ToString();

                //matricule.Substring(10,13);
                //mateleve3.Text = "6-61035362";
                //string matricule = mateleve3.Text + mateleve.Text;


                //string matricule = dataGridView1[0, dep].Value.ToString();

                //matricule.Substring(10,14);
                mateleve.Text = dataGridView1[0, dep].Value.ToString();
                nom.Text = dataGridView1[1, dep].Value.ToString();
                postnom.Text = dataGridView1[2, dep].Value.ToString();
                prenom.Text = dataGridView1[3, dep].Value.ToString();
                combosexe.Text = dataGridView1[4, dep].Value.ToString();
                datenaiss.Text = dataGridView1[5, dep].Value.ToString();
                comboavenue.Text = dataGridView1[6, dep].Value.ToString();
                comboquartier.Text = dataGridView1[7, dep].Value.ToString();
                combocommune.Text = dataGridView1[8, dep].Value.ToString();
                comboville.Text = dataGridView1[9, dep].Value.ToString();
                combonation.Text = dataGridView1[10, dep].Value.ToString();
                tutaire.Text = dataGridView1[11, dep].Value.ToString();
                profession.Text = dataGridView1[12, dep].Value.ToString();
                numtutaire.Text = dataGridView1[13, dep].Value.ToString();
                lieu_naiss.Text = dataGridView1[14, dep].Value.ToString();
                //datenaiss.Text = dataGridView1[4, dep].Value.ToString();

                el1.affichephotoelve(mateleve.Text, photo);

            }
            catch
            {

            }

        }
              
        string telephone_tutaire;       

              
        private void deplacerecours(int dep)
        {


            try
            {
                if (dep == 0)
                {
                    btnsuivant6.Enabled = true;
                    btnprecedent6.Enabled = false;
                }
                else if (dep == gridcours.RowCount - 2)
                {
                    btnsuivant6.Enabled = false;
                    btnprecedent6.Enabled = true;
                }
                else if (dep > 0 & dep < gridcours.RowCount - 2)
                {
                    btnsuivant6.Enabled = true;
                    btnprecedent6.Enabled = true;
                }

                else
                {
                    btnsuivant6.Enabled = false;
                    btnprecedent6.Enabled = false;
                }

                codecours.Text = gridcours[0, dep].Value.ToString();
                cours.Text = gridcours[1, dep].Value.ToString();
                

            }
            catch
            {

            }

        }

        private void deplacerperiode(int dep)
        {


            try
            {
                if (dep == 0)
                {
                    btnsuivant7.Enabled = true;
                    btnprecedent7.Enabled = false;
                }
                else if (dep == gridperiode.RowCount - 2)
                {
                    btnsuivant7.Enabled = false;
                    btnprecedent7.Enabled = true;
                }
                else if (dep > 0 & dep < gridperiode.RowCount - 2)
                {
                    btnsuivant7.Enabled = true;
                    btnprecedent7.Enabled = true;
                }

                else
                {
                    btnsuivant7.Enabled = false;
                    btnprecedent7.Enabled = false;
                }

                codeperiode.Text = gridperiode[0, dep].Value.ToString();
                periode.Text = gridperiode[1, dep].Value.ToString();


            }
            catch
            {

            }

        }

        private void deplacercotation(int dep)
        {


            try
            {
                if (dep == 0)
                {
                    btnsuivant8.Enabled = true;
                    btnprecedent8.Enabled = false;
                }
                else if (dep == gridcotation.RowCount - 2)
                {
                    btnsuivant8.Enabled = false;
                    btnprecedent8.Enabled = true;
                }
                else if (dep > 0 & dep < gridcotation.RowCount - 2)
                {
                    btnsuivant8.Enabled = true;
                    btnprecedent8.Enabled = true;
                }

                else
                {
                    btnsuivant8.Enabled = false;
                    btnprecedent8.Enabled = false;
                }

                codecote.Text = gridcotation[3, dep].Value.ToString();
                cote.Text = gridcotation[4, dep].Value.ToString();
                txtcombo_periode_cote.Text = gridcotation[5, dep].Value.ToString();
                comboperiode.Text = gridcotation[6, dep].Value.ToString();
                combocours.Text = gridcotation[7, dep].Value.ToString();
                comboeleve.Text = gridcotation[0, dep].Value.ToString();



            }
            catch
            {

            }

        }


        private void deplacerEnseignant(int dep)
        {


            try
            {
                if (dep == 0)
                {
                    btnsuivant9.Enabled = true;
                    btnprecedent9.Enabled = false;
                }
                else if (dep == gridEnseignant.RowCount - 2)
                {
                    btnsuivant9.Enabled = false;
                    btnprecedent9.Enabled = true;
                }
                else if (dep > 0 & dep < gridEnseignant.RowCount - 2)
                {
                    btnsuivant9.Enabled = true;
                    btnprecedent9.Enabled = true;
                }

                else
                {
                    btnsuivant9.Enabled = false;
                    btnprecedent9.Enabled = false;
                }

                txtMatriculeEns.Text = gridEnseignant[0, dep].Value.ToString();
                txtNomEns.Text = gridEnseignant[1, dep].Value.ToString();
                txtpostnomEns.Text = gridEnseignant[2, dep].Value.ToString();
                txtprenomEns.Text = gridEnseignant[3, dep].Value.ToString();
                txtsexeEns.Text = gridEnseignant[4, dep].Value.ToString();
                txtmailEns.Text = gridEnseignant[5, dep].Value.ToString();
                txtphoneEns.Text = gridEnseignant[6, dep].Value.ToString();
                txtdomaineEns.Text = gridEnseignant[7, dep].Value.ToString();
                txtqualifEns.Text = gridEnseignant[8, dep].Value.ToString();
                txtetatcivilEns.Text = gridEnseignant[9, dep].Value.ToString();

                el1.affichephotoEns(txtMatriculeEns.Text,photoEns);

            }
            catch
            {

            }

        }


        private void deplacerAffectation(int dep)
        {


            try
            {
                if (dep == 0)
                {
                    btnsuivant10.Enabled = true;
                    btnprecedent10.Enabled = false;
                }
                else if (dep == gridaffectation.RowCount - 2)
                {
                    btnsuivant10.Enabled = false;
                    btnprecedent10.Enabled = true;
                }
                else if (dep > 0 & dep < gridaffectation.RowCount - 2)
                {
                    btnsuivant10.Enabled = true;
                    btnprecedent10.Enabled = true;
                }

                else
                {
                    btnsuivant10.Enabled = false;
                    btnprecedent10.Enabled = false;
                }

                codeaffect.Text = gridaffectation[0, dep].Value.ToString();
                max_affect.Text = gridaffectation[1, dep].Value.ToString();
                cours_affect.Text = gridaffectation[2, dep].Value.ToString();
                //annee_affect.Text = gridaffectation[3, dep].Value.ToString();
                txtcombo_annee_affect.Text= gridaffectation[3, dep].Value.ToString();
                enseignant_affect.Text = gridaffectation[5, dep].Value.ToString();
                txtcombo_ens_affect.Text= gridaffectation[4, dep].Value.ToString();               
                classe_affect.Text = gridaffectation[7, dep].Value.ToString();
                txtcombo_periode_affect.Text= gridaffectation[13, dep].Value.ToString();
                combocode_periode_affect.Text = gridaffectation[14, dep].Value.ToString();
                txtcombo_section_affect.Text = gridaffectation[9, dep].Value.ToString();
                combo_section_affect.Text = gridaffectation[10, dep].Value.ToString();
                txtcombo_option_affect.Text = gridaffectation[11, dep].Value.ToString();
                combocode_option_affect.Text = gridaffectation[12, dep].Value.ToString();
                cmbEcoleAffect.Text= gridaffectation["nomEcol", dep].Value.ToString();
                txtcomboEcoleAffect.Text = gridaffectation["RefEcole", dep].Value.ToString();
                //el1.affichephotoEns(matEns.Text, photoEns);

            }
            catch
            {

            }

        }


        private void deplacer_detail_horaire(int dep)
        {            
        }       

        private void navigationPage2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            navframe.SelectedPage = pageeleve;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            navframe.SelectedPage = acceuil;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {           
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {          
        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {          
        }

        private void simpleButton22_Click(object sender, EventArgs e)
        {            
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
        }

        private void groupControl5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton27_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            navframe.SelectedPage = parametreanneee;
        }

        private void simpleButton46_Click(object sender, EventArgs e)
        {
            navframe.SelectedPage = acceuil;
        }

        private void simpleButton42_Click(object sender, EventArgs e)
        {
            
            try {
                bool teste = ClIntelligence.GetInstance().teste_Option(cmbOptionPrev.Text, txtComboSectionPrev.Text);
                if (radioanne.Checked)
                {
                    try
                    {
                        if (txtcodePrev.Text == "")
                        {
                            MessageBox.Show("Completer tous les champs svp !!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }

                        else
                        {

                            try
                            {

                                if (IsNumeric(txtcodePrev.Text))
                                {

                                    par1.insertionannee(int.Parse(txtcodePrev.Text), txtMontantPrev.Text);
                                    par1.chargementanne(dataGridView9);
                                    el1.chargementcombocodeannee(cmbAnneePrev);
                                    el1.chargementcombo_annee_designe(combo_anne_affect_horaire);
                                    //initialiserparame3();
                                }
                                else
                                {
                                    MessageBox.Show("Pas de lettre pour le code de l'annee !!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                }

                            }

                            catch (Exception)
                            {
                                MessageBox.Show("Pas de lettre pour le code l'annee !!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                            }

                        }


                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.Message);
                    }




                }

                else if (radioprevision.Checked)
                {
                    if (txtComboFraisPrev.Text == "" | txtMontantPrev.Text == "" | txtComboAnneePrev.Text == "" | cmbClassePrev.Text == "" | txtComboSectionPrev.Text == ""|txtcomboOptionPrev.Text=="")
                    {
                        MessageBox.Show("Completer tous les champs svp !!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                    else
                    {
                        try
                        {
                            if (IsNumeric(txtMontantPrev.Text))
                            {
                                if (float.Parse(txtMontantPrev.Text) > 0)
                                {
                                    if (teste == true)
                                    {
                                        par1.insertionprevision_final1(float.Parse(txtMontantPrev.Text), int.Parse(txtComboAnneePrev.Text), cmbClassePrev.Text, txtComboFraisPrev.Text, txtcomboOptionPrev.Text, txtComboSectionPrev.Text, int.Parse(txtcomboEcolePrevision.Text));
                                        par1.chargementprevision(dataGridView9);
                                        //el1.chargementcombocodeprevision(comboprevision);
                                        initialiserparame3();
                                    }
                                    else
                                    {
                                        MessageBox.Show("L'Option ne correspond pas a la section svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Le montant doit superieur a 0 svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                }

                            }
                            else
                            {
                                MessageBox.Show("Le montant doit etre en en numerique svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }

                        }

                        catch (Exception)
                        {
                            MessageBox.Show("Entrez les nombres svp!!!!!!");
                            txtcodePrev.Text = "";
                            txtMontantPrev.Text = "";
                            cmbAnneePrev.Text = "";
                            cmbFraisPrev.Text = "";
                            cmbFraisPrev.Text = "";
                        }

                    }
                }

                else if (radio_frais.Checked)
                {


                    if (txtcodePrev.Text == "" | txtMontantPrev.Text == "")
                    {
                        MessageBox.Show("Completer tous les champs svp !!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                    else
                    {

                        try
                        {
                                par1.insertion_frais(txtcodePrev.Text, txtMontantPrev.Text);
                                par1.chargement_frais(dataGridView9);
                                par1.chargementcombo_fais(cmbFraisPrev);
                                el1.chargementcombotypefrais(cmbtypefrais);

                                initialiserparame3();

                          

                        }

                        catch (Exception)
                        {
                            MessageBox.Show("Pas de lettre pour le poid et le nombre !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            txtcodePrev.Text = "";
                            txtMontantPrev.Text = "";
                        }

                    }

                }

                else
                {
                    MessageBox.Show("FAITES UN CHOIX DE PARAMETRE SVP !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }





            }

            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
                     

            
        }

        void updateAdress()
        {
            ClsAdresse adresse = new ClsAdresse();

            if (txtDesigneAdress.Text == "" || txtCodeAdress.Text == "")
            {
                MessageBox.Show("Completez tous les champs svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
            else
            {
                adresse.Code1 = txtCodeAdress.Text;
                adresse.Designation = txtDesigneAdress.Text;


                if (radionation.Checked == true)
                {
                    ClIntelligence.GetInstance().update_Pays(adresse, "pays", "codepays", "pays");
                    GridAdressage.DataSource = par1.chargement_pays();
                }
                else
                {
                    if (txtcomboRefAdresse.Text == "")
                    {
                        MessageBox.Show("Completez tous les champs svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        adresse.Reference = int.Parse(txtcomboRefAdresse.Text);
                        if (radioville.Checked == true)
                        {
                            ClIntelligence.GetInstance().update_Adresse(adresse, "ville", "codeville", "ville", "codepays");
                            GridAdressage.DataSource = par1.chargement_ville();
                        }
                        else if (radiocom.Checked == true)
                        {
                            ClIntelligence.GetInstance().update_Adresse(adresse, "commune", "codecommune", "commune", "codeville");
                            GridAdressage.DataSource = par1.chargement_commune();
                        }
                        else if (radioquartier.Checked == true)
                        {
                            ClIntelligence.GetInstance().update_Adresse(adresse, "quartier", "codequartier", "quartier", "codecom");
                            GridAdressage.DataSource = par1.chargement_quartier();
                        }
                        else if (radioAvenue.Checked == true)
                        {
                            ClIntelligence.GetInstance().update_Adresse(adresse, "tAvenue", "codeAvenue", "DesigneAvenue", "refQuartier");
                            GridAdressage.DataSource = par1.chargement_Avenue();
                        }

                        else
                        {
                            MessageBox.Show("Selectionnez un Element svp", "Selection Obligatoire", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }

        }


        //====================================================================================================
        //=========================================================================================================
        //=========================================================================================================
        void chargementApplication() {
            load_message();
            ChampMatriculeEcole.Text = ClIntelligence.GetInstance().RetourCodeEcole();

            ClIntelligence.GetInstance().chargementcombo_Ecole_designe(cmbEcoleHoraire);
            ClIntelligence.GetInstance().chargementcombo_Ecole_designe(cmbEcolePrevision);
            ClIntelligence.GetInstance().chargementcombo_Ecole_designe(cmbEcoleInscrip);
            ClIntelligence.GetInstance().chargementcombo_Ecole_designe(cmbEcoleAffect);

            cmbEcoleAffect.SelectedIndex = 0;
            cmbEcoleHoraire.SelectedIndex = 0;
            cmbEcoleInscrip.SelectedIndex = 0;

            par1.chargementcombocodeAvenue(comboavenue);
            par1.chargementclasse(gridparam1);
            par1.chargementcombo_section_designe(combo_section_inscription);
            par1.chargementcombo_section_designe(combo_section_affect);
            par1.chargementcombo_section_designe(combo_code_section_affect_horaire);
            UserSession.GetInstance().Annee = txtcomboAnneeDebut.Text;
            //codepay3.Enabled = true;
            el1.rechercheinscription(gridrecherchepaiement, txtcomboAnneeDebut.Text);
            el1.chargementeleve(dataGridView1);
            //el1.chargementcombocommune(combocommune);
            //el1.chargementcomboquartier(comboquartier);
            //el1.chargementcomboville(comboville);
            el1.chargementcombonation(combonation);
            el1.chargementcombocodeclasse2(comboclasse2);
            el1.chargementcombo_annee_designe(comboannee2);
            //par1.chargementcombo_option_designe(combo_option_inscription);
            el1.chargementcombo_annee_designe(annee_affect);
            el1.chargementinscription(dataGridView2,txtcomboAnneeDebut.Text);
            
            el1.chargementcombo_inscription(cmbElevePaie);
            //ens.chargementcombocodecompte(comboutilisateur);
            //text_param_prev4
            el1.chargementcombotypefrais(cmbtypefrais);
            el1.chargementpaiement(gridpaiement,txtcomboAnneeDebut.Text);
            cr1.chargementcours(gridcours);
            cr1.chargementperiode(gridperiode);
            cr1.chargementcombocours(cours_affect);
            cr1.chargementcomboeleve(comboeleve);
            cr1.chargementcombo_periode_designe(comboperiode);
            cr1.chargementcotation(gridcotation, txtcomboAnneeDebut.Text);
            cr1.chargementcombo_enseignant_designe(enseignant_affect);
            cr1.chargementaffectation(gridaffectation, txtcomboAnneeDebut.Text);
            el1.chargementcombocodeclasse2(classe_affect);
            cr1.chargementcombo_periode_designe(combocode_periode_affect);
            //par1.chargementcombo_option_designe(combocode_option_affect);

           
            
            //cr1.chargement_code_eleve_cote(combo_ele);


            par1.chargementcombo_section_designe(cmbSectionBulletin);
            par1.chargementcombo_option_designe(cmbOptionBulletin);
            ClIntelligence.GetInstance().chargementcombo_classe_designe(cmbClasseBulletin);
            ClIntelligence.GetInstance().chargementcombo_annee_designe(cmbanneeBulletin);
            ClIntelligence.GetInstance().chargementcombo_annee_designe(cmbannee2Bulletin);
            ClIntelligence.GetInstance().chargementcombo_annee_designe(cmbAnneeDebut);

            el1.chargementcombocodeclasse2(combo_classe_affect_horaire);
            //par1.chargementcombo_option_designe(combo_option_affect_horaire);
            el1.chargementcombo_annee_designe(combo_anne_affect_horaire);
            cr1.chargementcombo_enseignant_designe(combo_ens_affect_horaire);
            cr1.chargementcombocours(combo_cours_affect_horaire);
            
            //cr1.chargementcomboenseignant(combo_ens_gestionnaire);

            hor1.chargementcombo_jours_designe(comb_jours_affect_horaire);
            bib1.chargementcombocodelivre(combo_code_livre);
            hor1.chargement_horaire(gridhoraire,txtcomboAnneeDebut.Text);
            hor1.chargement_affectation_horaire(grid_affectation_horaire);

            //ens.chargementcombocodecomptable(combo_code_comptable_entree);
            //ens.chargementcombocodecomptable(combo_comptable_sortie);
            ////ens.chargementcombo_utilisateur_login(name);

            ens.chargementEns(gridEnseignant);
            gridControl4.DataSource = ens.chargement_comptable();
            gridControl3.DataSource = ens.chargement_solde_caisse();
            gridControl1.DataSource = ens.chargement_ressource();



            gridControl10.DataSource = bib1.chargement_recherche_emprunt_livre(txtcomboAnneeDebut.Text);
            gridControl_recherche_inscription_emprunt.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
            gridControl5.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
            //gridControl_em.DataSource = bib1.chargement_emprunt();
            gridControl2.DataSource = ens.chargement_depense();
            gridControl7.DataSource = ens.chargement_utilisateur();
            gridControl6.DataSource = bib1.chargement_livre();
            gridControl9.DataSource = bib1.chargement_emprunt(txtcomboAnneeDebut.Text);
            gridControl11.DataSource = bib1.chargement_remise_livre(txtcomboAnneeDebut.Text);
            gridControl12.DataSource = cr1.chargement_proclamation();
            
            par1.chargementcombo_section_designe(combo_section_pourcent);
            par1.chargementcombo_ption_cours(combo_option_pourcent);
            par1.chargementcombo_classe_pourcent(combo_classe_pourcent);
            cr1.chargementcombo_periode_pourcent(combo_periode_pourcent);

            bib1.chargementcombocodelivre(cmbLivreRetour);

            gridControl8.DataSource = ClIntelligence.GetInstance().chargementCours();
            gridControl15.DataSource = ClIntelligence.GetInstance().chargementCours();
            gridControl13.DataSource = ClIntelligence.GetInstance().chargementEnseignant();
            gridControl14.DataSource = ClIntelligence.GetInstance().chargementLivre();
        }
        //====================================================================================================
        //=========================================================================================================
        //=========================================================================================================
        private void Ecole1_Load(object sender, EventArgs e)
        {            
            pubCon.testFile();
            Ecole1 ec = new Ecole1();
            ec.Text = "";
            btnrestauration.Enabled = false;
            defaultLookAndFeel1.LookAndFeel.SkinName = "Blue";
            BtnPageParamRestauration.Enabled = false;

            radio_parent.Checked = true;
            radio_choix.Checked = true;

            txtdatepay.Text = DateTime.Now.ToString();

            navBarControl1.Enabled = false;
            cboPort.Enabled = false;
            btnconnect.Enabled = false;
            cmbAnneeDebut.Enabled = false;

            label_telephone.Visible = false;
            label_telephone_nom.Visible = false;
            label_telephone_postnom.Visible = false;
            radioprevision.Checked = true;
            timer1.Start();
            navframe.SelectedPage = acceuil;
            BaredeMenu.Enabled = false;            
            radio_tous_bulletin.Checked = true;
            controlAcces();            
            radio_humanite.Checked = true;
            param4.Enabled = false;
            param3.Enabled = false;            
        }

        void initialiserparame3() {
            txtcodePrev.Text = "";
            txtMontantPrev.Text = "";
            cmbAnneePrev.Text = "";
            cmbClassePrev.Text = "";
            cmbFraisPrev.Text = "";
            cmbOptionPrev.Text = "";

        }
        private void simpleButton43_Click(object sender, EventArgs e)
        {
            //parametre par1 = new parametre();
            if (radioanne.Checked)
            {
                if (txtcodePrev.Text == "")
                {
                    MessageBox.Show("Entrez le code de l'annee a supprimer !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                {                   
                    par1.supprimerannee(txtcodePrev.Text);
                    par1.chargementanne(dataGridView9);
                    el1.chargementcombo_annee_designe(comboannee2);
                    el1.chargementcombo_annee_designe(annee_affect);
                    el1.chargementcombo_annee_designe(combo_anne_affect_horaire);
                    initialiserparame3();

                    el1.chargementcombocodeannee(cmbAnneePrev);
                }



            }

            else if (radioprevision.Checked)
            {
                try
                {
                    if (txtcodePrev.Text == "")
                    {
                        MessageBox.Show("Entrez le code de la prevision a supprimer", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else
                    {
                        par1.supprimerprevision(txtcodePrev.Text);
                        par1.chargementprevision(dataGridView9);
                        //el1.chargementcombocodeprevision(te);
                        initialiserparame3();
                    }
                }
                catch {
                    MessageBox.Show("Erreur de suppression !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                


            }
            else if (radio_frais.Checked)
            {

                try
                {
                    if (txtcodePrev.Text == "")
                    {
                        MessageBox.Show("Entrez le code du frais a supprimer !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                    else
                    {
                        par1.supprimer_frais(txtcodePrev.Text);
                        par1.chargement_frais(dataGridView9);
                        par1.chargementcombo_fais(cmbFraisPrev);

                        el1.chargementcombotypefrais(cmbtypefrais);
                    }

                }
                catch {
                    MessageBox.Show("Erreur de suppression !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                

            }

            else {
                MessageBox.Show("Choisissez une option svp !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }            
        }

        private void simpleButton28_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton29_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton32_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton33_Click(object sender, EventArgs e)
        {
        }
        private void simpleButton30_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton31_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton34_Click(object sender, EventArgs e)
        {
           
        }

        private void simpleButton35_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton40_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton41_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton38_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton39_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {   
            try
            {

                if (mateleve.Text == "" | nom.Text == "" | postnom.Text == "" | prenom.Text == "" | combosexe.Text == "" | datenaiss.Text == "" | comboavenue.Text == "" | comboquartier.Text == "" | combocommune.Text == "" | comboville.Text == "" | combonation.Text == "" | tutaire.Text == "" | profession.Text == "" | numtutaire.Text == "" | lieu_naiss.Text == "")
                {
                    MessageBox.Show("Completez tous les champs !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                {
                    el1.mergeeleve(mateleve.Text, nom.Text, postnom.Text, prenom.Text, combosexe.Text, datenaiss.Text, comboavenue.Text, comboquartier.Text, combocommune.Text, comboville.Text, combonation.Text, tutaire.Text, profession.Text, numtutaire.Text, photo.Image, lieu_naiss.Text);
                    el1.chargementeleve(dataGridView1);
                    mateleve.Text = "";
                    nom.Text = ""; postnom.Text = ""; prenom.Text = "";
                    combosexe.Text = ""; datenaiss.Text = "";
                    comboavenue.Text = "";
                    //quartier.Text = "";
                    combocommune.Text = ""; comboville.Text = "";
                    combonation.Text = ""; tutaire.Text = "";
                    profession.Text = ""; numtutaire.Text = "";
                    photo.Image = Image.FromFile(Environment.CurrentDirectory + @"\library\elementary_school.png");

                    deplacerclient(dep);
                }
            }
            catch {
                MessageBox.Show("Erreur de sauvegarde !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }


            

        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            try
            {
                if (mateleve.Text == "")
                {
                    MessageBox.Show("Entrez le matricule de l'eleve a supprimer !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                {
                    el1.supprimereleve(mateleve.Text);
                    el1.chargementeleve(dataGridView1);
                }

            }
            catch {
                MessageBox.Show("Erreur de suppression !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
           
            
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show(this, "Voulez-vous Ajouter un eleve ?", "Innitialisation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                try
                {
                    mateleve.Text = "";
                    nom.Text = ""; postnom.Text = ""; prenom.Text = "";
                    combosexe.Text = ""; datenaiss.Text = "";
                    comboavenue.Text = ""; comboquartier.Text = "";
                    combocommune.Text = ""; comboville.Text = "";
                    combonation.Text = ""; tutaire.Text = "";
                    profession.Text = ""; numtutaire.Text = "";

                    photo.Image = Image.FromFile(Environment.CurrentDirectory + @"\library\elementary_school.png"); 
                    //photo.Image = null;

                }
                catch
                {
                    MessageBox.Show("Erreur d'innitialiser", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }



        
        
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            el1.affichephotoelve(mateleve.Text, photo);
            //affichePhoto2();
            dep = e.RowIndex;
            deplacerclient(dep);

        }

        private void rechercheeleve1_TextChanged(object sender, EventArgs e)
        {
            el1.rechercheeleve1(rechercheeleve1.Text,dataGridView1);
            el1.affichephotoelve(mateleve.Text, photo);
            deplacerclient(dep);

        }

        private void rechercheeleve2_TextChanged(object sender, EventArgs e)
        {
            el1.rechercheeleve2(rechercheeleve2.Text,txtRechercheInscription);
            el1.affichephotoelve2(mateleve2.Text, photo1);
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
        }

        private void btnsuivant2_Click(object sender, EventArgs e)
        {           
        }

        private void btnprecedent2_Click(object sender, EventArgs e)
        {           
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        { 
        }

        private void btnsuivant3_Click(object sender, EventArgs e)
        {            
        }

        private void btnprecedent3_Click(object sender, EventArgs e)
        {            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            el1.affichephotoelve2(mateleve2.Text, photo1);            
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            mateleve2.Text = "";
            comboclasse2.Text = "";
            comboannee2.Text = "";
            combo_section_inscription.Text = "";
            combo_division_inscription.Text = "";
            date_inscription.Text = "";
            combo_option_inscription.Text = "";
            code_inscription.Text = "";

            photo1.Image = Image.FromFile(Environment.CurrentDirectory + @"\library\elementary_school.png");



        }

        private void rechercheinscription_TextChanged(object sender, EventArgs e)
        {            
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            if (mateleve2.Text == "")
            {
                MessageBox.Show("Entrez le code l'inscription !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            else {
                try
                {
                    if (mateleve2.Text == "")
                    {
                        MessageBox.Show("Entrez le code l'inscription a supprimer !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else
                    {
                        el1.supprimereinscription(mateleve2.Text);
                        el1.chargementinscription(dataGridView2, txtcomboAnneeDebut.Text);
                        el1.chargementcombo_inscription(cmbElevePaie);
                        gridControl_recherche_inscription_emprunt.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
                        //gridControl_emprunt.DataSource = bib1.chargement_emprunt();
                    }


                }
                catch {
                    MessageBox.Show("Erreur de suppression !!!!1");
                }

                
            }
           

        }

        private void mateleve_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void postnom_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void avenue_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void groupControl8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioclasse_CheckedChanged(object sender, EventArgs e)
        {
           
            param3.Enabled=false;

            labelcode.Text = "Code classe";
            labeldesignation.Text = "Designation";
            labelcode2.Text = "";
            param1.Text = "";
            param2.Text = "";
            param3.Text = "";
            param4.Text = "";

            //labelcode2.Visible = false;
            par1.chargementclasse(gridparam1);

        }

        private void radiooption_CheckedChanged(object sender, EventArgs e)
        {
            param3.Enabled = true;
            //labelcode2.Visible = true;
            labelcode.Text = "Code option: ";
            labeldesignation.Text = "Designation: ";
            labelcode2.Text = "Code Option: ";
            param4.Visible = false;
            param3.Visible = true;
            param1.Text = "";
            param2.Text = "";
            param3.Text = "";
            param4.Text = "";
            //par1.chargementcombocodeclasse(param3);
            par1.chargementoption(gridparam1);
            par1.chargementcombocodesection(param3);
        }

        private void radiosection_CheckedChanged(object sender, EventArgs e)
        {
            param3.Enabled = true;
            //labelcode2.Visible = true;
            labelcode.Text = "Code section";
            labeldesignation.Text = "Designation";
            labelcode2.Text = "";
            param3.Visible = false;
            param4.Visible = true;
            param4.Enabled = true;
            param1.Text = "";
            param2.Text = "";
            param3.Text = "";
            param4.Text = "";
            //par1.chargementcombocodeoption(param4);
            par1.chargementsection(gridparam1);
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (radioclasse.Checked)
            {
                if (param1.Text==""|param2.Text=="") {
                    MessageBox.Show("Completer tous les champs !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else {
                    try {

                      
                            par1.insertionclasse(param1.Text, param2.Text);
                            par1.chargementclasse(gridparam1);
                            el1.chargementcombocodeclasse2(classe_affect);
                            el1.chargementcombocodeclasse2(comboclasse2);                            
                            par1.chargementcombo_classe_pourcent(combo_classe_pourcent);                        
                            el1.chargementcombocodeclasse2(JOURS);
                            par1.chargementcombocodeclasse(cmbClassePrev);
                            par1.chargementcombocodeclasse(combo_classe_affect_horaire);

                            param1.Text = "";
                            param2.Text = "";
                       
                    }

                    catch  {
                        MessageBox.Show("Le code de la classe doit etre en numerique !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    



                }


                
            }

            else if (radiooption.Checked)
            {

                if (param1.Text == "" | param2.Text == "" | param3.Text == "")
                {
                    MessageBox.Show("Completez tous les champs svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else {

                    try
                    {
                            par1.insertionoption(param1.Text, param2.Text, param3.Text);
                            par1.chargementoption(gridparam1);
                            par1.chargementcombo_option_designe(combocode_option_affect);
                            par1.chargementcombo_option_designe(combo_option_affect_horaire);
                            //par1.chargementcombo_section_cours(combo_section_cours);                            
                            par1.chargementcombo_ption_cours(combo_option_pourcent);

                            //par1.chargementcombocodeoption(text_param_prev4);
                            param1.Text = "";
                            param2.Text = "";
                            param3.Text = "";
                      

                    }
                    catch(Exception) {
                        MessageBox.Show("Le code de l'option doit etre en numerique svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                   
                    

                }

            }

            else if (radiosection.Checked)
            {

                if (param1.Text == "" | param2.Text == "")
                {
                    MessageBox.Show("Completez tous les champs svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else {
                    try
                    {
                      
                            par1.insertionsection(param1.Text, param2.Text);
                            par1.chargementsection(gridparam1);                                                    
                            par1.chargementcombo_section_designe(combo_section_pourcent);
                            par1.chargementcombo_section_designe(combo_code_section_affect_horaire);
                            param1.Text = "";
                            param2.Text = "";
                            param4.Text = "";
                        
                        
                    }
                    catch {
                        MessageBox.Show("Pas de Lettre pour le code de la section !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                 
                }
                

            }

            else {
                MessageBox.Show("Selectionner une option svp", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            param1.Text = "";
            param2.Text = "";
            param3.Text = "";
        }

        private void groupControl16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton26_Click(object sender, EventArgs e)
        {
            if (radioclasse.Checked) {
                try
                {

                    par1.supprimerclasse(param1.Text);
                    par1.chargementclasse(gridparam1);
                    el1.chargementcombocodeclasse2(classe_affect);
                    el1.chargementcombocodeclasse2(comboclasse2);
                    //hor1.chargementcombo_horaire(code_horaire_affect);
                    el1.chargementcombocodeclasse2(JOURS);
                    par1.chargementcombocodeclasse(cmbClassePrev);
                    par1.chargementcombocodeclasse(combo_classe_affect_horaire);
                }
                catch {
                    MessageBox.Show("Erreur de suppression !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }          

            }

            else if (radiooption.Checked) {
                try
                {

                    par1.supprimeroption(param1.Text);
                    par1.chargementoption(gridparam1);
                    par1.chargementcombo_option_designe(combocode_option_affect);
                    par1.chargementcombo_option_designe(combo_option_affect_horaire);
                    //par1.chargementcombocodeoption(text_param_prev4);
                    //par1.chargementcombo_section_cours(combo_section_cours);
                    
                }
                catch {
                   MessageBox.Show("Erreur de suppression !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                
            }

            else if (radiosection.Checked)
            {
                try
                {
                    par1.supprimersection(param1.Text);
                    par1.chargementsection(gridparam1);                    
                    par1.chargementcombo_section_designe(combo_code_section_affect_horaire);
                    par1.chargementcombocodesection(cmbSectionPrev);
                    //par1.chargementcombo_ption_cours(combo_option_cours);
                }
                catch {
                    MessageBox.Show("Erreur de Suppression !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
               
            }
            else
            {

                MessageBox.Show("Choisissez une option svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }


        }

        private void radionation_CheckedChanged(object sender, EventArgs e)
        {
            labelid.Text = "Code pays";
            labeldesignation2.Text = "Nationalite";
            labelid2.Text = "";
            cmbRefAdresse.Enabled = false;
            initialiserparame2();
            GridAdressage.DataSource = par1.chargement_pays();
            

        }

        private void simpleButton19_Click(object sender, EventArgs e)
        {
            try
            {
                billet_inscription rpt = new billet_inscription();
                rpt.DataSource = clreport.GetInstance().liste_inscription("billet_inscription", code_inscription.Text);
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }

        void initialiserparame2() {

            txtCodeAdress.Text = "";
            txtDesigneAdress.Text = "";
            cmbRefAdresse.Text = "";
        }
        private void radioville_CheckedChanged(object sender, EventArgs e)
        {
            labelid.Text = "Code ville";
            labeldesignation2.Text = "Designation";
            labelid2.Text = "codepays";
            cmbRefAdresse.Enabled = true;
            par1.chargementcombocodepays(cmbRefAdresse);
            initialiserparame2();
            GridAdressage.DataSource = par1.chargement_ville();
        }

        private void radiocom_CheckedChanged(object sender, EventArgs e)
        {
            labelid.Text = "Code commune";
            labeldesignation2.Text = "Designation";
            labelid2.Text = "code ville";
            cmbRefAdresse.Enabled = true;
            par1.chargementcombocodeville(cmbRefAdresse);
            initialiserparame2();
            GridAdressage.DataSource = par1.chargement_commune();
        }

        private void radioquartier_CheckedChanged(object sender, EventArgs e)
        {
            labelid.Text = "Code quartier";
            labeldesignation2.Text = "Designation";
            labelid2.Text = "code commune";
            cmbRefAdresse.Enabled = true;
            par1.chargementcombocodecommune(cmbRefAdresse);
            initialiserparame2();
            GridAdressage.DataSource = par1.chargement_quartier();
        }

        private void simpleButton25_Click(object sender, EventArgs e)
        {
            if (radionation.Checked) {
                try
                {
                    par1.insertionpays(txtCodeAdress.Text, txtDesigneAdress.Text);
                    el1.chargementcombonation(combonation);
                    initialiserparame2();
                }
                catch {
                    MessageBox.Show("Erreur de Sauvegarde !!!");
                }
                
            }

            else if (radioville.Checked) {
                try
                {
                    par1.insertionville(txtCodeAdress.Text, txtDesigneAdress.Text, cmbRefAdresse.Text);
                    el1.chargementcomboville(comboville);
                    initialiserparame2();
                }
                catch {
                    MessageBox.Show("Erreur de Sauvegarde !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                

            }

            else if (radiocom.Checked) {
                try
                {
                    par1.insertioncommine(txtCodeAdress.Text, txtDesigneAdress.Text, cmbRefAdresse.Text);
                    el1.chargementcombocommune(combocommune);
                    initialiserparame2();
                }
                catch {
                    MessageBox.Show("Erreur de Sauvegarde !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                
            }

            else if (radioquartier.Checked) {
                try
                {
                    par1.insertionquartier(txtCodeAdress.Text, txtDesigneAdress.Text, cmbRefAdresse.Text);
                    el1.chargementcomboquartier(comboquartier);
                    initialiserparame2();
                }
                catch {
                    MessageBox.Show("Erreur de Sauvegarde !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }               

            }




        }

        private void rechercheeleve_paie_TextChanged(object sender, EventArgs e)
        {            
        }

        private void rechercheeleve_paie_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnsuivant4_Click(object sender, EventArgs e)
        {            
        }

        private void btnprecedent4_Click(object sender, EventArgs e)
        {           
        }

        private void gridrecherchepaiement_CellClick(object sender, DataGridViewCellEventArgs e)
        {           
        }

        private void simpleButton21_Click(object sender, EventArgs e)
        {
            
        }

        void initialiser_paie() {

            txtcodePaiement.Text = "";
            txtmontantpay.Text = "";
            //txtdatepay.Text = "";
            cmbElevePaie.Text = "";
            //comboprevision.Text = "";
            //comboutilisateur.Text = "";
            cmbtypefrais.Text = "";
            //txtlibelle.Text = "";
            txtcomboTypeFrais.Text = "";
            //txtcomboComptablePaie.Text = "";
        }

        private void simpleButton20_Click(object sender, EventArgs e)
        {
            initialiser_paie();
        }

        private void btnprecedent5_Click(object sender, EventArgs e)
        {            
        }

        private void gridpaiement_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
        }

        private void recherchepaiement_TextChanged(object sender, EventArgs e)
        {            
        }

        ClsAdresse adresse = new ClsAdresse();
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label_prevision_section.Visible = false;
            cmbSectionPrev.Visible = false;

            labelparam4.Text = "Code:";
            labelparam5.Text = "Annee:";
            txtMontantPrev.Enabled = true;
            cmbAnneePrev.Enabled = false;
            cmbClassePrev.Enabled = false;
            cmbFraisPrev.Enabled = false;
            cmbOptionPrev.Enabled = false;
            label_param_prev1.Text = "";
            label_param_prev2.Text = "";
            label_param_prev3.Text = "";
            label_param_prev4.Text = "";
            par1.chargementanne(dataGridView9);

            txtcodePrev.Text = "";
            txtMontantPrev.Text = "";
            cmbAnneePrev.Visible = false;
            cmbClassePrev.Visible = false;
            cmbFraisPrev.Visible = false;
            cmbSectionPrev.Visible = false;
            cmbOptionPrev.Visible = false;
            cmbEcolePrevision.Visible = false;
            txtComboSectionPrev.Visible = false;
            txtcomboOptionPrev.Visible = false;
            txtcomboEcolePrevision.Visible = false;
            txtComboAnneePrev.Visible = false;
            txtComboFraisPrev.Visible = false;
            labelEcole.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            labelparam4.Text = "Code:";
            labelparam5.Text = "Montant:";
            
            txtMontantPrev.Enabled = true;
            cmbAnneePrev.Enabled = true;
            cmbClassePrev.Enabled = true;
            cmbFraisPrev.Enabled = true;
            cmbOptionPrev.Enabled = true;
            label_param_prev1.Text = "Année:";
            label_param_prev2.Text = "Classe:";
            label_param_prev3.Text = "Frais :";
            label_param_prev4.Text = "Option :";
            txtMontantPrev.Enabled = true;
            label_prevision_section.Visible = true;
            cmbSectionPrev.Visible = true;

            par1.chargementprevision(dataGridView9);
            cmbAnneePrev.Items.Clear();
            cmbClassePrev.Items.Clear();
            cmbSectionPrev.Items.Clear();
            cmbOptionPrev.Items.Clear();
            cmbFraisPrev.Items.Clear();
            el1.chargementcombo_annee_designe(cmbAnneePrev);            
            par1.chargementcombocodeclasse(cmbClassePrev);

            par1.chargementcombo_section_designe(cmbSectionPrev);
            //par1.chargementcombo_option_designe(text_param_prev4);          

            par1.chargementcombo_fais(cmbFraisPrev);


            txtcodePrev.Text = "";
            txtMontantPrev.Text = "";
            cmbAnneePrev.Visible = true;
            cmbClassePrev.Visible = true;
            cmbFraisPrev.Visible = true;
            cmbSectionPrev.Visible = true;
            cmbOptionPrev.Visible = true;
            cmbEcolePrevision.Visible = true;
            txtComboSectionPrev.Visible = true;
            txtcomboOptionPrev.Visible = true;
            txtcomboEcolePrevision.Visible = true;
            txtComboAnneePrev.Visible = true;
            txtComboFraisPrev.Visible = true;
            labelEcole.Visible = true;


        }

        private void ribbonStatusBar1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton24_Click(object sender, EventArgs e)
        {
           
        }

        private void simpleButton31_Click_1(object sender, EventArgs e)
        {
            navframe.SelectedPage = acceuil;
        }

        private void simpleButton32_Click_1(object sender, EventArgs e)
        {           
        }

        private void simpleButton33_Click_1(object sender, EventArgs e)
        {            
        }

        private void pagecotation_Click(object sender, EventArgs e)
        {           
        }

        private void simpleButton35_Click_1(object sender, EventArgs e)
        {
            codecours.Text = "";
            cours.Text = "";
            codecours.Enabled = false;
        }

        private void simpleButton34_Click_1(object sender, EventArgs e)
        {
            try
            {
                cr1.insertioncours(cours.Text);
                cr1.chargementcombocours(cours_affect);
                cr1.chargementcours(gridcours);
                cr1.chargementcombocours(combocours);
                cr1.chargementcombocours(combo_cours_affect_horaire);
                
            }
            catch {
                MessageBox.Show("Erreur de Sauvegarde !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }         


        }

        private void simpleButton36_Click_1(object sender, EventArgs e)
        {
            try
            {

                cr1.supprimercours(codecours.Text);
                gridControl12.DataSource = cr1.chargement_proclamation();
                cr1.chargementcours(gridcours);
                cr1.chargementcombocours(cours_affect);
                cr1.chargementcombocours(combocours);
                

            }
            catch  {
                MessageBox.Show("Erreur de Suppession  !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

           

        }

        private void recherchecours_TextChanged(object sender, EventArgs e)
        {
            
            cr1.recherchecours(recherchecours.Text,gridcours);
            deplacerecours(dep);
        }

        private void btnprecedent6_Click(object sender, EventArgs e)
        {
            dep--;
            deplacerecours(dep);
        }

        private void btnsuivant6_Click(object sender, EventArgs e)
        {
            dep++;
            deplacerecours(dep);
        }

        private void simpleButton38_Click_1(object sender, EventArgs e)
        {
            codeperiode.Text = "";
            periode.Text = "";
        }

        private void simpleButton39_Click_1(object sender, EventArgs e)
        {
           
            if (codeperiode.Text == "" | periode.Text == "")
            {

                MessageBox.Show("Completez tous les champs svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            else {
                try
                {
                    if (IsNumeric(codeperiode.Text))
                    {
                        cr1.insertionperiode(int.Parse(codeperiode.Text), periode.Text);
                        codeperiode.Text = "";
                        periode.Text = "";
                        cr1.chargementcombo_periode_designe(combocode_periode_affect);
                        cr1.chargementcombo_periode_pourcent(combo_periode_pourcent);

                        cr1.chargementperiode(gridperiode);

                        cr1.chargementcombo_periode_designe(comboperiode);
                    }
                    else {
                        MessageBox.Show("Pas de lettre pour le code de la periode !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Pas de lettre pour le code de la periode !!!!" + ex.Message);
                }


                
            }
            
        }

        private void simpleButton40_Click_1(object sender, EventArgs e)
        {
            try
            {

                cr1.modifierperiode(periode.Text, codeperiode.Text);
                cr1.chargementperiode(gridperiode);
                cr1.chargementcombo_periode_pourcent(combo_periode_pourcent);

                cr1.chargementcombo_periode_designe(combocode_periode_affect);

                cr1.chargementcombo_periode_designe(comboperiode);
                codeperiode.Text = "";
                periode.Text = "";
            }

            catch(Exception ex) {

                MessageBox.Show("Erreur de modification !!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void simpleButton41_Click_1(object sender, EventArgs e)
        {
            try
            {
                cr1.supprimerperiode(codeperiode.Text);
                cr1.chargementperiode(gridperiode);
                cr1.chargementcombo_periode_designe(combocode_periode_affect);
                cr1.chargementcombo_periode_pourcent(combo_periode_pourcent);
                cr1.chargementcombo_periode_designe(comboperiode);
                codeperiode.Text = "";
                periode.Text = "";
            }
            catch {
                MessageBox.Show("Erreur de suppression !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }           

        }

        private void textEdit3_TextChanged(object sender, EventArgs e)
        {
            cr1.rechercheperiode(rechercheperiode.Text,gridperiode);
            deplacerperiode(dep);
        }

        private void gridperiode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dep = e.RowIndex;
            deplacerperiode(dep);
        }

        private void gridcours_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dep = e.RowIndex;
            deplacerecours(dep);
        }

        private void simpleButton44_Click(object sender, EventArgs e)
        {
            try
            {
                cr1.modifiercours(cours.Text, codecours.Text);
                cr1.chargementcours(gridcours);
                cr1.chargementcombocours(combocours);
                cr1.chargementcombocours(cours_affect);
                cr1.chargementcombocours(combo_cours_affect_horaire);                
                codecours.Text = "";
                cours.Text = "";

            }
            catch {
                MessageBox.Show("Erreur de modification !!!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }

            
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {            
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {            
        }

        private void pagecotation1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton47_Click(object sender, EventArgs e)
        {
            try
            {

                cr1.insertioncotation(double.Parse(cote.Text), int.Parse(comboperiode.Text), combocours.Text, int.Parse(comboeleve.Text));
                cr1.chargementcotation(gridcotation, txtcomboAnneeDebut.Text);
                //cr1.chargement_code_eleve_cote(combo_ele);
                gridControl12.DataSource = cr1.chargement_proclamation();
                //nouvelle_cotation();
            }

            catch {
                MessageBox.Show("Erreur de Sauvegarde !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void simpleButton48_Click(object sender, EventArgs e)
        {
           
        }

        private void simpleButton49_Click(object sender, EventArgs e)
        {
            try
            {
                cr1.supprimercotation(codecote.Text);
                gridControl12.DataSource = cr1.chargement_proclamation();
                cr1.chargementcotation(gridcotation, txtcomboAnneeDebut.Text);
                //cr1.chargement_code_eleve_cote(combo_ele);
                //nouvelle_cotation();
            }
            catch {
                MessageBox.Show("Erreur de suppression !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            
        }

        private void recherchecote_TextChanged(object sender, EventArgs e)
        {
            cr1.recherchecotation(recherchecote.Text,gridcotation,UserSession.GetInstance().Annee);
            //deplacercotation(dep);
        }

        private void btnprecedent8_Click(object sender, EventArgs e)
        {
            dep--;
            deplacercotation(dep);
        }

        private void btnsuivant8_Click(object sender, EventArgs e)
        {
            dep++;
            deplacercotation(dep);

        }
        void nouvelle_cotation() {

            codecote.Text = "";
            cote.Text = "";
            comboperiode.Text = "";
            combocours.Text = "";
            comboeleve.Text = "";
            txtcombo_periode_cote.Text = "";

        }
        private void simpleButton45_Click(object sender, EventArgs e)
        {
            nouvelle_cotation();
        }

        private void gridcotation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dep = e.RowIndex;
            deplacercotation(dep);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            navframe.SelectedPage = pagecours;
        }

        private void simpleButton52_Click(object sender, EventArgs e)
        {
            //framepersonnel.SelectedPage = acceuil_personnel;
        }

        private void simpleButton51_Click(object sender, EventArgs e)
        {
            navframe.SelectedPage = acceuil;
        }

        private void simpleButton28_Click_1(object sender, EventArgs e)
        {
            navframe.SelectedPage = Page_Personel;
        }

        private void simpleButton54_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtMatriculeEns.Text == "" | txtNomEns.Text == "" | txtpostnomEns.Text == "" | txtprenomEns.Text == "" | txtsexeEns.Text == "" | txtmailEns.Text == "" | txtphoneEns.Text == "" | txtdomaineEns.Text == "" | txtqualifEns.Text == "" | txtetatcivilEns.Text == "")
                {
                    MessageBox.Show("Completez tous les champs svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                {
                    ens.mergeenseignant(txtMatriculeEns.Text, txtNomEns.Text, txtpostnomEns.Text, txtprenomEns.Text, txtsexeEns.Text, txtmailEns.Text, txtphoneEns.Text, txtdomaineEns.Text, txtqualifEns.Text, txtetatcivilEns.Text, photoEns.Image);
                    ens.chargementEns(gridEnseignant);
                    cr1.chargementcombo_enseignant_designe(enseignant_affect);
                    cr1.chargementcombo_enseignant_designe(combo_ens_affect_horaire);
                    //cr1.chargementcomboenseignant(combo_ens_gestionnaire);
                }

            }

            catch {
                MessageBox.Show("Erreur d'enregistrement !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            
        }

        private void btnprecedent9_Click(object sender, EventArgs e)
        {
            dep--;
            deplacerEnseignant(dep);
        }

        private void btnsuivant9_Click(object sender, EventArgs e)
        {
            dep++;
            deplacerEnseignant(dep);
        }

        void initialiserEnseignant() {

            if (MessageBox.Show(this, "Voulez-vous Ajouter un eleve ?", "Innitialisation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                try
                {
                    txtMatriculeEns.Text = "";
                    txtNomEns.Text = ""; txtpostnomEns.Text = ""; txtprenomEns.Text = "";
                    txtsexeEns.Text = ""; txtmailEns.Text = "";
                    txtphoneEns.Text = ""; txtdomaineEns.Text = "";
                    txtqualifEns.Text = ""; txtetatcivilEns.Text = "";
                    

                    photoEns.Image = Image.FromFile(Environment.CurrentDirectory + @"\library\elementary_school.png");
                    //photo.Image = null;

                }
                catch
                {
                    MessageBox.Show("Erreur d'innitialiser", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }




        }

        private void simpleButton53_Click(object sender, EventArgs e)
        {
            initialiserEnseignant();
        }

        private void simpleButton55_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMatriculeEns.Text == "")
                {
                    MessageBox.Show("Entrez le code de l'Enseignant a supprimer !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else {
                    ens.supprimerEns(txtMatriculeEns.Text);
                    ens.chargementEns(gridEnseignant);
                    cr1.chargementcombo_enseignant_designe(enseignant_affect);
                    cr1.chargementcombo_enseignant_designe(combo_ens_affect_horaire);                    
                }
                
            }
            catch{
                MessageBox.Show("Erreur de suppression !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            
        }

        private void gridEnseignant_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dep = e.RowIndex;
            el1.affichephotoEns(txtMatriculeEns.Text, photoEns);
            deplacerEnseignant(dep);
        }

        private void textEdit9_TextChanged(object sender, EventArgs e)
        {
            ens.rechercheEns(rechercheEns.Text,gridEnseignant);
            deplacerEnseignant(dep);
        }

        private void groupControl27_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton56_Click(object sender, EventArgs e)
        {
            //framecours.SelectedPage = page_affectation;
        }

        private void simpleButton58_Click(object sender, EventArgs e)
        {            
        }

        private void rechercheaffect_TextChanged(object sender, EventArgs e)
        {
            cr1.rechercheaffectation(rechercheaffect.Text,gridaffectation, UserSession.GetInstance().Annee);
        }

        private void simpleButton60_Click(object sender, EventArgs e)
        {
            try
            {
                if (codeaffect.Text == "")
                {

                    MessageBox.Show("Completer le code de l'affectation a supprimer !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                {
                    cr1.supprimeraffectation(codeaffect.Text);
                    gridControl12.DataSource = cr1.chargement_proclamation();
                    cr1.chargementaffectation(gridaffectation, txtcomboAnneeDebut.Text);
                }


            }
            catch  {
                MessageBox.Show("Erreur de Suppression !!!!1", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            
        }

        private void simpleButton59_Click(object sender, EventArgs e)
        {            
        }

        void initialiserAffectation(){
            codeaffect.Text = "";
            max_affect.Text = "";
            annee_affect.Text = "";
            cours_affect.Text = "";
            enseignant_affect.Text = "";
            combo_section_affect.SelectedIndex=-1;
            combocode_option_affect.SelectedIndex = -1;
            combocode_periode_affect.SelectedIndex = -1;

            txtcombo_section_affect.Text = "";
            txtcombo_ens_affect.Text = "";
            txtcombo_annee_affect.Text = "";
            txtcombo_option_affect.Text = "";
            txtcombo_periode_affect.Text = "";
            cmbEcoleAffect.SelectedIndex = 0;
            //txtcomboEcoleAffect.Text = "";

        }
        private void simpleButton57_Click(object sender, EventArgs e)
        {
            initialiserAffectation();
        }

        private void gridaffectation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dep = e.RowIndex;
            deplacerAffectation(dep);
        }

        private void btnprecedent10_Click(object sender, EventArgs e)
        {
            dep--;
            deplacerAffectation(dep);
        }

        private void btnsuivant10_Click(object sender, EventArgs e)
        {
            dep++;
            deplacerAffectation(dep);
        }

        private void groupControl14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton66_Click(object sender, EventArgs e)
        {
            try
            {
                //hor1.insertion_detail_horaire(lundi.Text, mardi.Text, mercredi.Text, jeudi.Text, vendredi.Text, samedi.Text);
                //hor1.chargement_detail_horaire(griddetail);
                //hor1.chargementcombo_detail(combocode_detail);
            }
            catch  {
                MessageBox.Show("Erreur de Sauvegarde !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

           
        }

        private void recherche_detail_TextChanged(object sender, EventArgs e)
        {            
        }

        private void simpleButton68_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex) {
                MessageBox.Show("Erreur de Suppression !!!!"+ex.Message);
            }

           
        }

        private void simpleButton67_Click(object sender, EventArgs e)
        {

            try
            {                
            }
            catch(Exception ex) {
                MessageBox.Show("Erreur de Modification !!!!1"+ex.Message);
            }
            
           
        }

        private void griddetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dep = e.RowIndex;
            deplacer_detail_horaire(dep);
        }

        private void btnprecedent11_Click(object sender, EventArgs e)
        {
            dep--;
            deplacer_detail_horaire(dep);
        }

        private void btnsuivant11_Click(object sender, EventArgs e)
        {
            dep++;
            deplacer_detail_horaire(dep);
        }

        void initialiser_detail() {
           

        }

        private void simpleButton65_Click(object sender, EventArgs e)
        {
            initialiser_detail();
        }

        private void simpleButton62_Click(object sender, EventArgs e)
        {
            
                    }

        private void simpleButton63_Click(object sender, EventArgs e)
        {
           
        }

        void initialiser_horaire() {
            code_affect_horaire.Text = "";
            combo_anne_affect_horaire.Text = "";
            combo_classe_affect_horaire.Text = "";
            combo_ens_affect_horaire.Text = "";
            heure_debut.Text = "";
            heure_fin.Text = "";
            combo_option_affect_horaire.Text = "";
            combo_ens_affect_horaire.Text = "";
            comb_jours_affect_horaire.Text = "";
            combo_cours_affect_horaire.Text = "";
            combo_code_section_affect_horaire.Text = "";
            combo_division_horaire.Text = "";

        }
        private void simpleButton71_Click(object sender, EventArgs e)
        {
            initialiser_horaire();
        }

        private void simpleButton73_Click(object sender, EventArgs e)
        {
        }

        private void recherchehoraire_TextChanged(object sender, EventArgs e)
        {            
        }

        private void gridhoraire_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
        }

        private void btnprecedent12_Click(object sender, EventArgs e)
        {           
        }

        private void btnsuivant12_Click(object sender, EventArgs e)
        {            
        }

        private void simpleButton74_Click(object sender, EventArgs e)
        {
            if (code_affect_horaire.Text == "")
            {
                XtraMessageBox.Show("Entrez le code l'affectation de l'horaire a supprimer !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            else {
                try
                {
                    hor1.supprimer_horaire(code_affect_horaire.Text);
                    hor1.chargement_horaire(gridhoraire,txtcomboAnneeDebut.Text);
                    hor1.chargementcombo_jours_designe(comb_jours_affect_horaire);
                }
                catch 
                {
                    XtraMessageBox.Show("Erreur de Suppression  !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
           
        }

        private void simpleButton72_Click(object sender, EventArgs e)
        {          
        }

        private void simpleButton64_Click(object sender, EventArgs e)
        {
            //framehoraire.SelectedPage = page_affect_horaire;
        }

        private void simpleButton70_Click(object sender, EventArgs e)
        {
            try {
                hor1.insertion_affectation_horaire(int.Parse(code_jours.Text), JOURS.Text);
                hor1.chargement_affectation_horaire(grid_affectation_horaire);
                hor1.chargementcombo_jours_designe(comb_jours_affect_horaire);
            }
            catch {
                MessageBox.Show("Erreur d'enregistrement !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            
        }

        private void recherche_affect_recherche_TextChanged(object sender, EventArgs e)
        {
            hor1.recherche_affectation_horaire(recherche_affect_recherche.Text,grid_affectation_horaire);
        }

        private void simpleButton76_Click(object sender, EventArgs e)
        {
            try
            {

                hor1.supprimer_affectation_horaire(code_jours.Text);
                hor1.chargement_affectation_horaire(grid_affectation_horaire);
                hor1.chargementcombo_jours_designe(comb_jours_affect_horaire);
            }
            catch {
                XtraMessageBox.Show("Erreur de suppression !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            
        }

        private void simpleButton75_Click(object sender, EventArgs e)
        {
            try
            {
                hor1.modifier_affectation_horaire(int.Parse(code_jours.Text), int.Parse(JOURS.Text));
                hor1.chargement_affectation_horaire(grid_affectation_horaire);
                hor1.chargementcombo_jours_designe(comb_jours_affect_horaire);
            }

            catch {
                XtraMessageBox.Show("Erreur de modification !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            
        }

        void initialiser_affectation_horaire() {
            JOURS.Text = "";
            code_jours.Text = "";
        }
        private void simpleButton69_Click(object sender, EventArgs e)
        {
            initialiser_affectation_horaire();
        }

        private void simpleButton77_Click(object sender, EventArgs e)
        {
            navframe.SelectedPage = acceuil;
        }

        private void simpleButton29_Click_1(object sender, EventArgs e)
        {
            navframe.SelectedPage = page_horaire;
        }

        private void simpleButton78_Click(object sender, EventArgs e)
        {
            try
            {
                Carte_eleve rpt = new Carte_eleve();
                rpt.DataSource = clreport.GetInstance().Identite_eleve("Eleve", mateleve.Text);
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }
        }

        private void simpleButton79_Click(object sender, EventArgs e)
        {
            try
            {
                carte_service rpt = new carte_service();
                rpt.DataSource = clreport.GetInstance().Identite_enseignant("Enseignant", txtMatriculeEns.Text);
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }
        }

        private void groupControl36_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label_prevision_section.Visible = false;
            cmbSectionPrev.Visible = false;

            labelparam4.Text = "Code:";
            labelparam5.Text = "Frais:";
            txtMontantPrev.Enabled = true;
            //cmbAnneePrev.Enabled = false;
            //cmbClassePrev.Enabled = false;
            //cmbFraisPrev.Enabled = false;
            //cmbOptionPrev.Enabled = false;
            label_param_prev1.Text = "";
            label_param_prev2.Text = "";
            label_param_prev3.Text = "";
            label_param_prev4.Text = "";
            par1.chargement_frais(dataGridView9);

            txtcodePrev.Text = "";
            txtMontantPrev.Text = "";
            cmbAnneePrev.Visible=false;
            cmbClassePrev.Visible = false;
            cmbFraisPrev.Visible = false;
            cmbSectionPrev.Visible = false;
            cmbOptionPrev.Visible = false;
            cmbEcolePrevision.Visible = false;
            txtComboSectionPrev.Visible = false;
            txtcomboOptionPrev.Visible = false;
            txtcomboEcolePrevision.Visible = false;
            txtComboAnneePrev.Visible = false;
            txtComboFraisPrev.Visible = false;
            labelEcole.Visible = false;


        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void inscriptioneleve_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton80_Click(object sender, EventArgs e)
        {         


        }

        private void gridaffectation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void simpleButton91_Click(object sender, EventArgs e)
        {
            navframe.SelectedPage = acceuil;
        }

        private void simpleButton22_Click_1(object sender, EventArgs e)
        {
            navframe.SelectedPage = page_bibliotheque;
        }

        private void simpleButton87_Click(object sender, EventArgs e)
        {           
        }

        private void simpleButton89_Click(object sender, EventArgs e)
        {            
        }

        private void simpleButton88_Click(object sender, EventArgs e)
        {            
        }

        private void simpleButton90_Click(object sender, EventArgs e)
        {            
        }

        private void simpleButton84_Click(object sender, EventArgs e)
        {
            try
            {
                bib1.insertion_livre(codelivre.Text, titre_livre.Text, auteur_livre.Text, etat_livre.Text, double.Parse(nombre_livre.Text));

                gridControl6.DataSource = bib1.chargement_livre();
                bib1.chargementcombocodelivre(combo_code_livre);
                //gridControl_recherche_inscription.DataSource = el1.chargement_recherche_inscription();
                gridControl_recherche_inscription_emprunt.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
               // gridControl_emprunt.DataSource = bib1.chargement_emprunt();
            }

            catch(Exception ex) {

                XtraMessageBox.Show("Erreur d'Enregistrement !!!!1" +ex.Message);
            }

            


        }

        private void simpleButton85_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton86_Click(object sender, EventArgs e)
        {
            try
            {
                bib1.supprimer_livre(codelivre.Text);
                bib1.chargementcombocodelivre(combo_code_livre);
                gridControl6.DataSource = bib1.chargement_livre();
                gridControl_recherche_inscription_emprunt.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
                
            }
            catch {

                XtraMessageBox.Show("Erreur de suppression !!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            
        }

        void initialiser_livre() {
            codelivre.Text = "";
            titre_livre.Text = "";
            auteur_livre.Text = "";
            etat_livre.Text = "";
            nombre_livre.Text = "";
        }

        private void simpleButton83_Click(object sender, EventArgs e)
        {
            initialiser_livre();
        }

        private void recherche_livre_TextChanged(object sender, EventArgs e)
        {            
        }

        private void btnprecedent13_Click(object sender, EventArgs e)
        {            
        }

        private void btnsuivant13_Click(object sender, EventArgs e)
        {          
        }
        private void gridlivre_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void cboPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPort.Text == "19")
            {
                portnumber.Text = "19";
            }
            else
            {
                portnumber.Text = cboPort.Text.Substring(3, 2);
            }
        }

        private void btndeconnect_Click(object sender, EventArgs e)
        {
            
        }

        bool envoie_Petitmessage()
        {
            bool envoie = false;
            Cursor.Current = Cursors.WaitCursor;
            try
            {               
                // Send an SMS message
                SmsSubmitPdu pdu;
                bool alert = chkAlert.Checked;
                bool unicode = chkUnicode.Checked;

                if (!alert && !unicode)
                {
                    // The straightforward version
                    pdu = new SmsSubmitPdu(MStexte.Text, Numtext.Text, "");  // "" indicate SMSC No
                }
                else
                {
                    // The extended version with dcs
                    byte dcs;
                    if (!alert && unicode)
                        dcs = DataCodingScheme.NoClass_16Bit;
                    else if (alert && !unicode)
                        dcs = DataCodingScheme.Class0_7Bit;
                    else if (alert && unicode)
                        dcs = DataCodingScheme.Class0_16Bit;
                    else
                        dcs = DataCodingScheme.NoClass_7Bit; // should never occur here

                    pdu = new SmsSubmitPdu(MStexte.Text, Numtext.Text, "", dcs);
                    
                }

                // Send the same message multiple times if this is set
                int times = chkMultipleTimes.Checked ? int.Parse(txtSendTimes.Text) : 1;

                if (!pubCon.comm.IsOpen())
                    pubCon.comm.Open();
                // Send the message the specified number of times
                for (int i = 0; i < times; i++)
                {
                    pubCon.comm.SendMessage(pdu);
                    Output(string.Format("Message {0} of {1} sent.\n", i + 1, times));
                    Output("");
                }

                XtraMessageBox.Show("Message envoyé avec succès", "Confirmation sending", MessageBoxButtons.OK, MessageBoxIcon.Information);
                envoie = true;
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                envoie = false;
            }

            Cursor.Current = Cursors.Default;

            return envoie;

        }

        private void simpleButton81_Click(object sender, EventArgs e)
        {
            if (radio_personnel.Checked && radio_tous.Checked)
            {
                if (MStexte.Text.Length < 140)
                {
                    envoiePlusieurPetitMessage(grid_personnel2, "numtel");
                }
                else
                {
                    envoiePlusieurGrandMessage(grid_personnel2, "numtel");
                }


                
            }

            else if (radio_parent.Checked && radio_tous.Checked)
            {

                if (MStexte.Text.Length < 140)
                {                    
                    envoiePlusieurPetitMessage(gridView_parent, "num_tutaire");
                }
                else
                {
                    envoiePlusieurGrandMessage(gridView_parent, "num_tutaire");
                }
                
            }
            else if (radio_personnel.Checked && radio_choix.Checked)
            {
                if (MStexte.Text.Length < 160)
                {
                    if (envoie_Petitmessage() == true)
                    {

                    }
                    else {
                        ClsMessagerieInsert m = new ClsMessagerieInsert();
                        m.Numero1 = Numtext.Text;
                        m.MessateTexte1 = MStexte.Text;
                        m.EtatSms1 = 0;
                        m.DateEvoie1 = DateTime.Now;
                        m.Utilisateur1 = LabelUser.Text;
                        ClIntelligence.GetInstance().insertMessagerie(m);
                    }
                }
                else
                {
                    if (envoie_une_personne() == true)
                    {

                    }
                    else {
                        ClsMessagerieInsert m = new ClsMessagerieInsert();
                        m.Numero1 = Numtext.Text;
                        m.MessateTexte1 = MStexte.Text;
                        m.EtatSms1 = 0;
                        m.DateEvoie1 = DateTime.Now;
                        m.Utilisateur1 = LabelUser.Text;
                        ClIntelligence.GetInstance().insertMessagerie(m);
                    }
                }                
            }
            else if (radio_parent.Checked && radio_choix.Checked)
            {
                if (MStexte.Text.Length < 160)
                {
                    if (envoie_Petitmessage() == true)
                    {

                    }
                    else
                    {
                        ClsMessagerieInsert m = new ClsMessagerieInsert();
                        m.Numero1 = Numtext.Text;
                        m.MessateTexte1 = MStexte.Text;
                        m.EtatSms1 = 0;
                        m.DateEvoie1 = DateTime.Now;
                        m.Utilisateur1 = LabelUser.Text;
                        ClIntelligence.GetInstance().insertMessagerie(m);
                    }
                }
                else
                {
                    if (envoie_une_personne() == true)
                    {

                    }
                    else
                    {
                        ClsMessagerieInsert m = new ClsMessagerieInsert();
                        m.Numero1 = Numtext.Text;
                        m.MessateTexte1 = MStexte.Text;
                        m.EtatSms1 = 0;
                        m.DateEvoie1 = DateTime.Now;
                        m.Utilisateur1 = LabelUser.Text;
                        ClIntelligence.GetInstance().insertMessagerie(m);
                    }
                }
               
            }

            else {
                XtraMessageBox.Show("Erreur d'Envoyer un message a l'Inconnu !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void simpleButton115_Click(object sender, EventArgs e)
        {           
        }

        private void simpleButton23_Click(object sender, EventArgs e)
        {
            navframe.SelectedPage = messagerie;
        }

        private void simpleButton114_Click(object sender, EventArgs e)
        {
            txtOutput.Text = "";
        }

        private void btndeconnect_Click_1(object sender, EventArgs e)
        {
            label_statut.Text = "Deconnecté";
            portnumber.Text = "||";
            cboPort.Text = "";
        }

        private void grid_message2_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
        }

        private void btnprecedent15_Click(object sender, EventArgs e)
        {            
        }

        private void btnsuivant15_Click(object sender, EventArgs e)
        {           
        }

        private void simpleButton115_Click_1(object sender, EventArgs e)
        {
            navframe.SelectedPage = acceuil;
        }

        private void radioButton4_CheckedChanged_1(object sender, EventArgs e)
        {
            gridControl_parent.Visible = true;
            gridControl_parent.DataSource = el1.chargementeleve_message(UserSession.GetInstance().Annee);
            grid_controle_personnel1.Visible = false;
            label_liste_personnel.Visible = false; 
            label_liste_parent.Visible = true;            
        }

        private void recherche_envoie_message_TextChanged(object sender, EventArgs e)
        {
        }

        private void radioButton3_CheckedChanged_2(object sender, EventArgs e)
        {
            grid_controle_personnel1.Visible = true;
            label_liste_personnel.Visible = true;
            label_liste_parent.Visible = false;
            //grid_envoie_message.DataSource = ens.chargementEns_message();
            grid_controle_personnel1.DataSource = el1.chargementEns_message();
            gridControl_parent.Visible = false;

        }

        private void btnprecedent16_Click(object sender, EventArgs e)
        {
        }

        private void btnsuivant16_Click(object sender, EventArgs e)
        {
        }

        private void grid_envoie_message_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void simpleButton93_Click(object sender, EventArgs e)
        {
            bib1.chargementcombocodelivre(combo_code_livre);
            gridControl_recherche_inscription_emprunt.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
           
        }

        private void simpleButton95_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton92_Click(object sender, EventArgs e)
        {
        }

        private void recherche_gestion_TextChanged(object sender, EventArgs e)
        {
        }       

        private void simpleButton116_Click(object sender, EventArgs e)
        {            
        }

        private void grid_controle_personnel1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Numtext.Text = grid_personnel2.GetFocusedRowCellValue("numtel").ToString();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void radioButton3_CheckedChanged_3(object sender, EventArgs e)
        {
            Numtext.Enabled = false;
        }

        private void radioButton4_CheckedChanged_2(object sender, EventArgs e)
        {
            Numtext.Enabled = true;
        }

        private void gridControl_parent_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Numtext.Text = gridView_parent.GetFocusedRowCellValue("num_tutaire").ToString();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

        }

        private void simpleButton82_Click(object sender, EventArgs e)
        {
            Numtext.Text = "";
            MStexte.Text = "";
        }

        private void simpleButton97_Click(object sender, EventArgs e)
        {           
        }

        private void simpleButton99_Click(object sender, EventArgs e)
        {
            try
            {
                bib1.supprimer_remise_livre(code_remise.Text);
                gridControl11.DataSource = bib1.chargement_remise_livre(txtcomboAnneeDebut.Text);
            }
            catch {
                XtraMessageBox.Show("Erreur de suppression !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            
        }

        private void simpleButton96_Click(object sender, EventArgs e)
        {
            code_remise.Text = "";
            combo_code_emprunt.Text = "";
            confirme_remise.Text = "";        
            

        }

        private void gridControl_recherche_inscription_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
              combo_code_emprunt.Text = gridView10.GetFocusedRowCellValue("num").ToString();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

        }

        private void gridControl_recherche_inscription_emprunt_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                combo_inscription_emprunt.Text = gridView_recherche_inscription_emprunt.GetFocusedRowCellValue("codeinscription").ToString();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void simpleButton111_Click(object sender, EventArgs e)
        {
            try
            {

                bib1.insertion_emprunt(int.Parse(combo_inscription_emprunt.Text), combo_code_livre.Text, DateTime.Parse(date_emprunt.Text), DateTime.Parse(date_retour.Text),int.Parse(txtnombreLivreEmprunt.Text));
                gridControl9.DataSource = bib1.chargement_emprunt(txtcomboAnneeDebut.Text);
                gridControl10.DataSource = bib1.chargement_recherche_emprunt_livre(txtcomboAnneeDebut.Text);
            }
            catch 
            {
                XtraMessageBox.Show("Erreur de sauvegarde !!!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }

        }

        private void simpleButton110_Click(object sender, EventArgs e)
        {
            try
            {
                bib1.supprimer_emprunt(code_emprunt.Text);
                gridControl9.DataSource = bib1.chargement_emprunt(txtcomboAnneeDebut.Text);
                gridControl10.DataSource = bib1.chargement_recherche_emprunt_livre(txtcomboAnneeDebut.Text);
            }
            catch {
                XtraMessageBox.Show("Erreur de suppression !!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            
        }

        private void simpleButton113_Click(object sender, EventArgs e)
        {
            code_emprunt.Text = "";
            combo_inscription_emprunt.Text = "";
            combo_code_livre.Text = "";
            date_emprunt.Text = "";
            date_retour.Text = "";
            txtnombreLivreEmprunt.Text = "";
        }

        private void gridControl_emprunt_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void simpleButton16_Click_1(object sender, EventArgs e)
        {
            navframe.SelectedPage = acceuil;
        }

        private void simpleButton30_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (IsNumeric(montant_ress.Text))
                {
                    if (double.Parse(montant_ress.Text) > 0)
                    {
                        ens.insertion_ressource(double.Parse(montant_ress.Text), motif_ress.Text, LabelUser.Text);
                        gridControl1.DataSource = ens.chargement_ressource();
                        gridControl3.DataSource = ens.chargement_solde_caisse();
                    }

                    else {
                        MessageBox.Show("Le montant ne doit pas etre negatif svp !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
                else {
                    MessageBox.Show("Le montant doit etre en numerique svp", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                
            }
            catch {
                XtraMessageBox.Show("Erreur de sauvegarde !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            
        }

        private void simpleButton61_Click(object sender, EventArgs e)
        {
            try
            {
                ens.supprimer_ressource(code_ressource.Text);
                gridControl1.DataSource = ens.chargement_ressource();
                gridControl3.DataSource = ens.chargement_solde_caisse();
            }
            catch {
                XtraMessageBox.Show("Erreur de suppression !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            
        }

        private void simpleButton50_Click(object sender, EventArgs e)
        {
            code_ressource.Text = "";
            montant_ress.Text = "";
            motif_ress.Text = "";
        }

        private void simpleButton100_Click(object sender, EventArgs e)
        {
            code_depense.Text = "";
            montant_depense.Text = "";
            motif_depense.Text = "";
            
        }

        private void simpleButton101_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsNumeric(montant_depense.Text))
                {
                    if (double.Parse(montant_depense.Text) < 0)
                    {
                        MessageBox.Show("Le montant ne doit pas etre negatif svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else {
                        string acqui = "OK";
                        ens.insertion_depense(double.Parse(montant_depense.Text), motif_depense.Text, montant_lettre.Text, faveur.Text, autoriser.Text, LabelUser.Text, acqui);
                        gridControl2.DataSource = ens.chargement_depense();
                        gridControl3.DataSource = ens.chargement_solde_caisse();

                    }

                }
                else {
                    MessageBox.Show("Le doit etre en numerique svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                

            }
            catch {
                XtraMessageBox.Show("Erreur de sauvegarde !!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            
        }

        private void simpleButton98_Click(object sender, EventArgs e)
        {
            try
            {
                ens.supprimer_depense(code_depense.Text);
                gridControl2.DataSource = ens.chargement_depense();
                gridControl3.DataSource = ens.chargement_solde_caisse();
            }
            catch {
                XtraMessageBox.Show("Erreur de suppression !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                code_ressource.Text = gridView1.GetFocusedRowCellValue("code_ent").ToString();
                montant_ress.Text = gridView1.GetFocusedRowCellValue("montant_ent").ToString();
                motif_ress.Text = gridView1.GetFocusedRowCellValue("motif_ent").ToString();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

        }

        private void gridControl2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                code_depense.Text = gridView2.GetFocusedRowCellValue("code_dep").ToString();
                montant_depense.Text = gridView2.GetFocusedRowCellValue("montant_dep").ToString();
                motif_depense.Text = gridView2.GetFocusedRowCellValue("motif_dep").ToString();
                montant_lettre.Text= gridView2.GetFocusedRowCellValue("montant_lettre").ToString();
                faveur.Text = gridView2.GetFocusedRowCellValue("faveur").ToString();
                autoriser.Text= gridView2.GetFocusedRowCellValue("autorisateur").ToString();
                //combo_comptable_sortie.Text = gridView2.GetFocusedRowCellValue("code_comptable").ToString();
                //pour_acquit.Text = gridView2.GetFocusedRowCellValue("pour_acquit").ToString();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btn_compabilite_Click(object sender, EventArgs e)
        {
            navframe.SelectedPage = comptabilite;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void page_affect_horaire_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton103_Click(object sender, EventArgs e)
        {           
            navframe.SelectedPage = pageemprunt_bibliotheque;
        }

        private void radio_co_CheckedChanged(object sender, EventArgs e)
        {
            combo_option_inscription.Enabled = false;
            combo_section_inscription.Enabled = false;           

        }

        private void radio_humanite_CheckedChanged(object sender, EventArgs e)
        {
            combo_option_inscription.Enabled = false;
            combo_section_inscription.Enabled = true;
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {


            try
            {
                if (code_inscription.Text == "")
                {
                    XtraMessageBox.Show("Entrez le code de l'inscription a supprimer !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                {
                    el1.supprimereinscription(code_inscription.Text);
                    el1.chargementinscription(dataGridView2, txtcomboAnneeDebut.Text);
                    el1.chargementcombo_inscription(cmbElevePaie);
                    bib1.chargementcombocodelivre(combo_code_livre);                    
                    gridControl_recherche_inscription_emprunt.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
                    
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Erreur de Suppression  !!!!!" + ex.Message);
            }

            
           
        }

        private void btn_historique_Click(object sender, EventArgs e)
        {
            navframe.SelectedPage = frame_historique;
        }

        private void simpleButton31_Click_2(object sender, EventArgs e)
        {
            navframe.SelectedPage= parametreanneee;
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                ens.insertion_utilisateur(int.Parse(code_util.Text),nom_util.Text, fonction_util.Text, pass_util.Text);
                gridControl7.DataSource = ens.chargement_utilisateur();
            }
            catch {
                XtraMessageBox.Show("Erreur de sauvegarde !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            try
            {
                ens.supprimer_utilisateur(code_util.Text);
                gridControl7.DataSource = ens.chargement_utilisateur();
            }
            catch {
                XtraMessageBox.Show("Erreuur de suppression !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            code_util.Text = "";
            nom_util.Text = "";
            fonction_util.Text = "";
            pass_util.Text = "";
        }

        private void gridControl7_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                code_util.Text = gridView7.GetFocusedRowCellValue("code").ToString();
                nom_util.Text = gridView7.GetFocusedRowCellValue("nom").ToString();
                fonction_util.Text = gridView7.GetFocusedRowCellValue("fonction").ToString();
                pass_util.Text= gridView7.GetFocusedRowCellValue("pass").ToString();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void simpleButton4_Click_2(object sender, EventArgs e)
        {
            navframe.SelectedPage = frame_ajouter_utilisateur;
        }

        private void simpleButton5_Click_1(object sender, EventArgs e)
        {
            try
            {

                liste_total_paiement rpt = new liste_total_paiement();
                rpt.DataSource = clreport.GetInstance().liste_total_paiement("total_paiement", int.Parse(codeaffect.Text));
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }
        }

        private void simpleButton7_Click_1(object sender, EventArgs e)
        {
           
        }

        private void radioButton3_CheckedChanged_4(object sender, EventArgs e)
        {           
            grid_historique.DataSource = el1.chargement_historique_inscription();
        }

        private void radioButton4_CheckedChanged_3(object sender, EventArgs e)
        {           
            grid_historique.DataSource = ens.chargement_historique_paiement();
        }

        private void radio_final_CheckedChanged(object sender, EventArgs e)
        {
            combo_section_inscription.Enabled = true;
            combo_option_inscription.Enabled = true;
        }

        private void comboclasse2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int choix = int.Parse(comboclasse2.Text);
            if (choix == 1 | choix == 2) {
                radio_co.Checked = true;
            }

            else if (choix==3 | choix==4) {
                radio_humanite.Checked = true;               
            }
            else if (choix == 5 | choix == 6)
            {
                radio_final.Checked = true;
            }


        }

        private void radio_raprt_co_CheckedChanged(object sender, EventArgs e)
        {            
        }

        private void radio_raport_moyen_CheckedChanged(object sender, EventArgs e)
        {            
        }

        private void radio_raport_final_CheckedChanged(object sender, EventArgs e)
        {            
        }

        private void combo_report_classe_SelectedIndexChanged(object sender, EventArgs e)
        {            
        }

        private void combo_report_option_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton7_Click_2(object sender, EventArgs e)
        {
          
        }

        private void classe_affect_SelectedIndexChanged(object sender, EventArgs e)
        {
            int choix = int.Parse(classe_affect.Text);
            if (choix == 1 | choix ==2) {
                radio_co_affect.Checked = true;
            }

            else if (choix == 3 | choix == 4)
            {
                radio_moyen_affect.Checked = true;
            }

            else if (choix == 5 | choix == 6)
            {
                radio_final_affect.Checked = true;
            }
        }

        private void radio_co_affect_CheckedChanged(object sender, EventArgs e)
        {
            combocode_option_affect.Enabled = false;
            combo_section_affect.Enabled = false;
        }

        private void radio_moyen_affect_CheckedChanged(object sender, EventArgs e)
        {
            combo_section_affect.Enabled = true;
            combocode_option_affect.Enabled = false;
        }

        private void radio_final_affect_CheckedChanged(object sender, EventArgs e)
        {
            combo_section_affect.Enabled = true;
            combocode_option_affect.Enabled = true;
        }

        private void gridControl5_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                comboeleve.Text = gridView5.GetFocusedRowCellValue("codeinscription").ToString();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void gridControl6_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                codelivre.Text = gridView6.GetFocusedRowCellValue("code_livre").ToString();
                titre_livre.Text = gridView6.GetFocusedRowCellValue("titre_livre").ToString();
                auteur_livre.Text = gridView6.GetFocusedRowCellValue("auteur").ToString();
                etat_livre.Text = gridView6.GetFocusedRowCellValue("etat_livre").ToString();
                nombre_livre.Text = gridView6.GetFocusedRowCellValue("nombre_livre").ToString();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void gridControl9_MouseClick(object sender, MouseEventArgs e)
        {

            try
            {
                code_emprunt.Text = gridView9.GetFocusedRowCellValue("num").ToString(); ;
                combo_inscription_emprunt.Text =gridView9.GetFocusedRowCellValue("ref_inscription").ToString(); ;
                combo_code_livre.Text = gridView9.GetFocusedRowCellValue("ref_livre").ToString();
                date_emprunt.Text = gridView9.GetFocusedRowCellValue("date_retrait").ToString();
                date_retour.Text = gridView9.GetFocusedRowCellValue("date_retour").ToString();
                txtnombreLivreEmprunt.Text = gridView9.GetFocusedRowCellValue("signature_eleve").ToString();

            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void gridControl11_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                code_remise.Text = gridView11.GetFocusedRowCellValue("code_retour").ToString();
                combo_code_emprunt.Text = gridView11.GetFocusedRowCellValue("ref_emprunt").ToString();
                confirme_remise.Text = gridView11.GetFocusedRowCellValue("signature_bibliothecaire").ToString();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void gridControl10_MouseClick(object sender, MouseEventArgs e)
        {

            try
            {
                combo_code_emprunt.Text = gridView10.GetFocusedRowCellValue("num").ToString();
                //confirme_remise.Text = gridView11.GetFocusedRowCellValue("signature_bibliothecaire").ToString();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void simpleButton109_Click(object sender, EventArgs e)
        {
            try
            {
                Carte_bibliotheque rpt = new Carte_bibliotheque();
                rpt.DataSource = clreport.GetInstance().liste_bibliotheque("emprunt_bibliotheque", int.Parse(code_emprunt.Text));
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }
        }

        private void simpleButton37_Click_1(object sender, EventArgs e)
        {           
        }

        private void combo_classe_cours_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void radio_co_cours_CheckedChanged(object sender, EventArgs e)
        {            
        }

        private void radio_moyen_cours_CheckedChanged(object sender, EventArgs e)
        {            
        }

        private void radio_final_cours_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_proclamation_Click(object sender, EventArgs e)
        {
            navframe.SelectedPage = frame_proclamation;
        }

        private void combo_classe_pourcent_SelectedIndexChanged(object sender, EventArgs e)
        {
            string choix = combo_classe_pourcent.Text;
            if (choix == "1ere" | choix == "2eme")
            {
                combo_section_pourcent.Enabled = false;
                combo_option_pourcent.Enabled = false;
            }

            else if (choix == "3eme" | choix == "4eme")
            {
                combo_section_pourcent.Enabled = true;
                combo_option_pourcent.Enabled = false;
            }
            else if (choix == "5eme" | choix == "6eme")
            {
                combo_section_pourcent.Enabled = true;
                combo_option_pourcent.Enabled = true;
            }

            else
            {

                XtraMessageBox.Show("Erreur de choix !!!");
            }

        }

        private void simpleButton8_Click_1(object sender, EventArgs e)
        {           
        }

        private void simpleButton12_Click_1(object sender, EventArgs e)
        {
            try
            {
                Borderau_sortie rpt = new Borderau_sortie();
                rpt.DataSource = clreport.GetInstance().borderau_sortie("view_depense", code_depense.Text);
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }
        }

        private void btnprecedent_Click(object sender, EventArgs e)
        {
            //dep--;
            dep--;
            deplacerclient(dep);

        }

        private void btnsuivant_Click(object sender, EventArgs e)
        {
            dep++;
            deplacerclient(dep);

        }

        private void simpleButton13_Click_1(object sender, EventArgs e)
        {
            gridControl6.DataSource = bib1.chargement_livre();
            bib1.chargementcombocodelivre(combo_code_livre);
            //gridControl_recherche_inscription.DataSource = el1.chargement_recherche_inscription();
            gridControl_recherche_inscription_emprunt.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
            
        }

        private void combo_classe_affect_horaire_SelectedIndexChanged(object sender, EventArgs e)
        {           

        }

        private void radio_co_horaire_CheckedChanged(object sender, EventArgs e)
        {
            combo_option_affect_horaire.Enabled = false;
            combo_code_section_affect_horaire.Enabled = false;
        }

        private void radio_moyen_horaire_CheckedChanged(object sender, EventArgs e)
        {
            combo_option_affect_horaire.Enabled = false;
            combo_code_section_affect_horaire.Enabled = true;
        }

        private void radio_final_horaire_CheckedChanged(object sender, EventArgs e)
        {
            combo_option_affect_horaire.Enabled = true;
            combo_code_section_affect_horaire.Enabled = true;
        }

        private void simpleButton14_Click_1(object sender, EventArgs e)
        {

            if (radio_eleve_bulletin.Checked == true)
            {
                if (combo_ele.Text == "" | txtcomboAnnee2Bulletin.Text=="")
                {
                    XtraMessageBox.Show("Selectionnez l'eleve dans la liste ci-haute svp !!!!!");
                }

                else
                {
                    try
                    {
                        BulletinPage rpt = new BulletinPage();
                        rpt.DataSource = clreport.GetInstance().billetin("Proclamation_Final", combo_ele.Text,txtcomboAnnee2Bulletin.Text);
                        using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                        {
                            printTool.ShowPreviewDialog();
                        }
                    }
                    catch (Exception ex)
                    { XtraMessageBox.Show("Erreur d'impression !!! Par de bulletin pour les information ci-dessus !!! veillet d'abord coter l'eleve svp !!!"); }
                }


            }

            else if (radio_tous_bulletin.Checked == true)
            {

                //imprimer_plus_bulletin();
                if (txtcomboAnnee2Bulletin.Text == "")
                {
                    MessageBox.Show("Entrez l'année svp !!!");
                }
                else {

                    try
                    {
                        BulletinPage rpt = new BulletinPage();
                        rpt.DataSource = clreport.GetInstance().billetinTous("Proclamation_Final",txtcomboAnnee2Bulletin.Text);
                        using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                        {
                            printTool.ShowPreviewDialog();
                        }
                    }
                    catch (Exception ex)
                    { XtraMessageBox.Show("Erreur d'impression !!! Par de bulletin pour les information ci-dessus !!! veillet d'abord coter l'eleve svp !!!"); }
                }


            }

            else {
                XtraMessageBox.Show("Choisissez une option SVP !!!!");
            }






        }

        private void simpleButton11_Click_1(object sender, EventArgs e)
        {

        }

        private void gridControl12_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                combo_ele.Text = gridView12.GetFocusedRowCellValue("Codeeleve").ToString();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        string fonction = "";
        string fonction_login;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                //cn.Open();
                SqlCommand cmd = new SqlCommand("exec SP_Login " + name.Text + "," + pass.Text + "", myconn);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                int count = 0;
                while (dr.Read())
                {
                    fonction = dr["fonction"].ToString();
                    fonction_login = fonction;
                    NomUser = dr["nom"].ToString();
                    UserSession.GetInstance().AccessLevel = dr["nom"].ToString();
                    count += 1;
                }

                if (count == 1)
                {
                    MessageBox.Show("La connection a reussie !!!!!!");
                    BaredeMenu.Enabled = true;
                    panel3.Visible = false;
                    fonction_login = fonction;
                    chargementApplication();
                    controlAcces();
                    Ecole1 ec = new Ecole1();
                    ec.Text = ClIntelligence.GetInstance().RetourneEcole();
                    cmbAnneeDebut.SelectedIndex = 0;

                    navBarControl1.Enabled = true;
                    cboPort.Enabled = true;
                    btnconnect.Enabled = true;
                    cmbAnneeDebut.Enabled = true;

                    name.Text = "";
                    pass.Text = "";
                }
                else if (count > 1)
                {
                    MessageBox.Show("duplicate");
                    //menu_principal.Enabled = true;
                    panel3.Visible = false;
                    fonction_login = fonction;
                    controlAcces();
                }
                else
                {

                    MessageBox.Show("Echec de connection !!!!!!!!!");
                }

                //username
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

        }

        void imprimer_plus_bulletin() {

            try
            {
                if (XtraMessageBox.Show("Voulez-vous confirmer l'Impression.?", "IMPRIMER TOUS LES BULLETTINS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                    if (gridView12.RowCount > 0)
                    {                       

                        for (int x = 0; x < gridView12.RowCount; x++)
                        {
                            string num = gridView12.GetRowCellValue(x, "Codeeleve").ToString();                            
                           
                                try
                                {
                                BulletinPage rpt = new BulletinPage();
                                    rpt.DataSource = clreport.GetInstance().billetin("Proclamation_Final", num,txtcomboAnnee2Bulletin.Text);
                                    using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                                    {
                                        printTool.ShowPreviewDialog();
                                    }
                                }
                                catch (Exception ex)
                                { XtraMessageBox.Show(ex.Message); }
                            


                        }

                    }
                    else
                    {
                        XtraMessageBox.Show("Aucun eleve trouve !!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }




        }
        private void simpleButton5_Click_2(object sender, EventArgs e)
        {
            imprimer_plus_bulletin();

        }

        private void radio_tous_bulletin_CheckedChanged(object sender, EventArgs e)
        {
            combo_ele.Enabled = false;
        }

        private void radio_eleve_bulletin_CheckedChanged(object sender, EventArgs e)
        {
            combo_ele.Enabled = false;
        }

        private void simpleButton16_Click_2(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Voulez-vous confirmer l'archivage des information.?", "ARCHIVER LES INFORMATIONS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                if (XtraMessageBox.Show("Voulez-vous mettre a jours les paiements, les cotations, la bibliotheque.?", "METTRE A JOURS LES INFORMATIONS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    if (XtraMessageBox.Show("Voulez-vous deplacer les informations des paiements, des cotaions,et de la biblitheque dans l'historique.?", "HISTORIQUE DE L'ECOLE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        try {
                ens.chargement_mise_a_jours();
            }
            catch (Exception ex) {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            //gridControl4.DataSource = null;
            grid_historique.DataSource = ens.chargement_historique_cotation();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            //gridControl4.DataSource = null;
            grid_historique.DataSource = ens.chargement_historique_bibliothque();
        }

        private void simpleButton5_Click_3(object sender, EventArgs e)
        {
            ////gridControl4.DataSource = null;
            //gridControl4.;
        }

        private void text_param_prev2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void recherche_historique_TextChanged(object sender, EventArgs e)
        {

            try {
                if (radio_historique_inscription.Checked == true)
                {
                    ens.recherche_historique_inscription(recherche_historique.Text, grid_historique);
                }

                else if (radio_historique_paiement.Checked == true)
                {
                    ens.recherche_historique_paiement(recherche_historique.Text, grid_historique);
                }

                else if (radio_historique_cotation.Checked == true)
                {
                    ens.recherche_historique_cotation(recherche_historique.Text, grid_historique);
                }

                else if (radio_historique_bibliotheque.Checked == true)
                {
                    ens.recherche_historique_bibliotheque(recherche_historique.Text, grid_historique);
                }

            }

            catch (Exception ex) {
                XtraMessageBox.Show(ex.Message);
            }

            

        }

        private void montantpay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void combocodeeleve3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void comboutilisateur_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void combotypefrais_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void combo_section_inscription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void combo_option_inscription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void comboclasse2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void comboannee2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void tparam1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void codecours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void codeperiode_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void cote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void codecote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void comboperiode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void comboeleve_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void codeaffect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void max_affect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void annee_affect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void classe_affect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void combo_section_affect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void combocode_option_affect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void combocode_periode_affect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void nombre_livre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void montant_ress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void montant_depense_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void combo_comptable_sortie_KeyPress(object sender, KeyPressEventArgs e)
        {         
        }

        private void code_depense_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void code_ressource_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetAllPorts(cboPort);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelControl1.Text =""+ DateTime.Now;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormConfig f2 = new FormConfig();
            f2.Show();

        }

        private void code_jours_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        void imprimerFiche()
        {

            try
            {
                livre rpt = new livre();
                rpt.DataSource = clreport.GetInstance().livre(DateTime.Parse(date1.Text),DateTime.Parse(date2.Text));

                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }

        }

        private void simpleButton11_Click_2(object sender, EventArgs e)
        {
            imprimerFiche();
        }

        private void simpleButton15_Click_1(object sender, EventArgs e)
        {            
        }

        private void simpleButton21_Click_1(object sender, EventArgs e)
        {
            ens.insertion_comptable(code_compte.Text,nom_compte.Text);
            gridControl4.DataSource = ens.chargement_comptable();
        }

        private void simpleButton22_Click_2(object sender, EventArgs e)
        {
            ens.supprimer_comptable(code_compte.Text);
            gridControl4.DataSource = ens.chargement_comptable();
        }

        private void simpleButton20_Click_1(object sender, EventArgs e)
        {
            code_compte.Text = "";
            nom_compte.Text = "";
        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }

        private void cours_affect_TextChanged(object sender, EventArgs e)
        {
            cr1.recherche_combo_cours(cours_affect,cours_affect.Text);
        }

        private void combocours_TextChanged(object sender, EventArgs e)
        {
            cr1.recherche_combo_cours(combocours, combocours.Text);
        }

        private void combotypefrais_SelectedIndexChanged(object sender, EventArgs e)
        {
            el1.sairie_code_frais(txtcomboTypeFrais,cmbtypefrais.Text);
        }

        private void comboutilisateur_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ens.saisir_code_comptable(txtcomboComptablePaie, comboutilisateur.Text);
        }

        private void barButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = parametreanneee;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = acceuil;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = pageenregistrement_eleve;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = pageeleve;
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = pageliste_cours;            
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = pagecours;
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = pageaffectation_enseignant;
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = pagecotation_eleve;
        }

        private void barButtonItem34_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = page_bibliotheque;
        }

        private void barButtonItem35_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = pageemprunt_bibliotheque;
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = pageremise_bibliotheque;
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = pagepaiement;
        }

        private void barButtonItem33_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = comptabilite;
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = pageenregistrement_jours;
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = page_horaire;
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = frame_proclamation;
        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = page_palmaress;
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = messagerie;
        }

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem36_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = Page_Personel;
        }

        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = pageenregistrement_comptable;
        }

        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = pageparametre_eleve;
        }

        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = frame_ajouter_utilisateur;
        }

        private void barButtonItem32_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Voulez-vous fermer la Connexion ?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes) {

                panel3.Visible = true;
                pubCon.testFile();
                Ecole1 ec = new Ecole1();
                ec.Text = "";
                btnrestauration.Enabled = false;
                defaultLookAndFeel1.LookAndFeel.SkinName = "Blue";
                BtnPageParamRestauration.Enabled = false;

                radio_parent.Checked = true;
                radio_choix.Checked = true;

                txtdatepay.Text = DateTime.Now.ToString();

                navBarControl1.Enabled = false;
                cboPort.Enabled = false;
                btnconnect.Enabled = false;
                cmbAnneeDebut.Enabled = false;

                label_telephone.Visible = false;
                label_telephone_nom.Visible = false;
                label_telephone_postnom.Visible = false;
                radioprevision.Checked = true;
                timer1.Start();
                navframe.SelectedPage = acceuil;
                BaredeMenu.Enabled = false;
                radio_tous_bulletin.Checked = true;
                controlAcces();
                radio_humanite.Checked = true;
                param4.Enabled = false;
                param3.Enabled = false;
            }

        }

        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = frame_historique;

        }

        private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = frame_historique;
        }

        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = frame_historique;
        }

        private void barButtonItem31_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = frame_historique;
        }

        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Voulez-vous confirmer l'archivage des information.?", "ARCHIVER LES INFORMATIONS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                if (XtraMessageBox.Show("Voulez-vous mettre a jours les paiements, les cotations, la bibliotheque.?", "METTRE A JOURS LES INFORMATIONS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    if (XtraMessageBox.Show("Voulez-vous deplacer les informations des paiements, des cotaions,et de la biblitheque dans l'historique.?", "HISTORIQUE DE L'ECOLE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        try
                        {
                            ens.chargement_mise_a_jours();
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message);
                        }
        }

        private void simpleButton74_Click_1(object sender, EventArgs e)
        {

        }

        private void label77_Click(object sender, EventArgs e)
        {

        }

        private void param4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gridEnseignant_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void simpleButton23_Click_1(object sender, EventArgs e)
        {
            liere_message();
        }

        private void simpleButton15_Click_2(object sender, EventArgs e)
        {
            Output1.Text = "";
        }

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = pagereceptionmessage;
        }

        private void grid_message2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dep = e.RowIndex;
            deplacer_message_read(dep);
        }

        private void btnprecedentM_Click(object sender, EventArgs e)
        {
            dep++;
            deplacer_message_read(dep);
        }

        private void btnsuivantM_Click(object sender, EventArgs e)
        {
            dep--;
            deplacer_message_read(dep);
        }

        private void radiooption_CheckedChanged_1(object sender, EventArgs e)
        {
            param3.Enabled = true;
            //labelcode2.Visible = true;
            labelcode.Text = "Code option: ";
            labeldesignation.Text = "Designation: ";
            labelcode2.Text = "Section: ";
            param4.Visible = false;
            param3.Visible = true;
            param1.Text = "";
            param2.Text = "";
            param3.Text = "";
            param4.Text = "";
            //par1.chargementcombocodeclasse(param3);
            par1.chargementoption(gridparam1);
            param3.Items.Clear();
            par1.chargementcombocodesection(param3);
        }

        private void radiosection_CheckedChanged_1(object sender, EventArgs e)
        {
            param3.Enabled = true;
            //labelcode2.Visible = true;
            labelcode.Text = "Code section";
            labeldesignation.Text = "Designation";
            labelcode2.Text = "";
            param3.Visible = false;
            param4.Visible = false;
            param4.Enabled = true;
            param1.Text = "";
            param2.Text = "";
            param3.Text = "";
            param4.Text = "";
            //par1.chargementcombocodeoption(param4);

            par1.chargementsection(gridparam1);
        }

        private void radioclasse_CheckedChanged_1(object sender, EventArgs e)
        {
            param3.Enabled = false;

            labelcode.Text = "Code classe";
            labeldesignation.Text = "Designation";
            labelcode2.Text = "";
            param1.Text = "";
            param2.Text = "";
            param3.Text = "";
            param4.Text = "";           
            par1.chargementclasse(gridparam1);
        }

        private void simpleButton10_Click_1(object sender, EventArgs e)
        {

            param1.Text = "";
            param2.Text = "";
            param3.Text = "";
        }

        private void simpleButton9_Click_1(object sender, EventArgs e)
        {
            if (radioclasse.Checked)
            {
                if (param1.Text == "" | param2.Text == "")
                {
                    XtraMessageBox.Show("Completer tous les champs !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {


                        par1.insertionclasse(param1.Text, param2.Text);
                        par1.chargementclasse(gridparam1);
                        el1.chargementcombocodeclasse2(classe_affect);
                        el1.chargementcombocodeclasse2(comboclasse2);

                        par1.chargementcombo_classe_pourcent(combo_classe_pourcent);


                        // hor1.chargementcombo_horaire(code_horaire_affect);
                        el1.chargementcombocodeclasse2(JOURS);

                        par1.chargementcombocodeclasse(cmbClassePrev);
                        par1.chargementcombocodeclasse(combo_classe_affect_horaire);

                        param1.Text = "";
                        param2.Text = "";

                    }

                    catch 
                    {
                        XtraMessageBox.Show("Le code de la classe doit etre en numerique !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                }

            }

            else if (radiooption.Checked)
            {

                if (param1.Text == "" | param2.Text == "" | param3.Text == "")
                {
                    XtraMessageBox.Show("Completez tous les champs svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                {

                    try
                    {
                        par1.insertionoption(param1.Text, param2.Text, param3.Text);
                        par1.chargementoption(gridparam1);
                        par1.chargementcombo_option_designe(combocode_option_affect);
                        par1.chargementcombo_option_designe(combo_option_affect_horaire);
                        par1.chargementcombo_option_designe(combo_option_inscription);
                        //par1.chargementcombo_section_cours(combo_section_cours);                        
                        par1.chargementcombo_ption_cours(combo_option_pourcent);

                        par1.chargementcombo_option_designe(cmbOptionPrev);
                        param1.Text = "";
                        param2.Text = "";
                        param3.Text = "";


                    }
                    catch (Exception)
                    {
                        XtraMessageBox.Show("Le code de l'option doit etre en numerique svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                }

            }

            else if (radiosection.Checked)
            {

                if (param1.Text == "" | param2.Text == "")
                {
                    XtraMessageBox.Show("Completez tous les champs svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {

                        par1.insertionsection(param1.Text, param2.Text);
                        par1.chargementsection(gridparam1);
                        //par1.chargementcombocodesection(combo_report_section);                        
                        par1.chargementcombo_section_designe(combo_section_inscription);
                        par1.chargementcombo_section_designe(combo_section_pourcent);
                        par1.chargementcombo_section_designe(combo_code_section_affect_horaire);
                        param1.Text = "";
                        param2.Text = "";
                        param4.Text = "";


                    }
                    catch
                    {
                        XtraMessageBox.Show("Pas de Lettre pour le code de la section !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                }


            }

            else
            {
                MessageBox.Show("Selectionner une option svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void simpleButton26_Click_1(object sender, EventArgs e)
        {
            if (radioclasse.Checked)
            {
                try
                {

                    par1.supprimerclasse(param1.Text);
                    par1.chargementclasse(gridparam1);
                    el1.chargementcombocodeclasse2(classe_affect);
                    el1.chargementcombocodeclasse2(comboclasse2);
                    //hor1.chargementcombo_horaire(code_horaire_affect);
                    el1.chargementcombocodeclasse2(JOURS);
                    par1.chargementcombocodeclasse(cmbClassePrev);
                    par1.chargementcombocodeclasse(combo_classe_affect_horaire);
                }
                catch
                {
                    XtraMessageBox.Show("Erreur de suppression !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }



            }

            else if (radiooption.Checked)
            {
                try
                {

                    par1.supprimeroption(param1.Text);
                    par1.chargementoption(gridparam1);
                    par1.chargementcombo_option_designe(combocode_option_affect);
                    par1.chargementcombo_option_designe(combo_option_inscription);
                    par1.chargementcombo_option_designe(combo_option_affect_horaire);
                    par1.chargementcombo_option_designe(cmbOptionPrev);
                    
                    //par1.chargementcombo_section_cours(combo_section_cours);
                    

                }
                catch
                {
                    XtraMessageBox.Show("Erreur de suppression !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

            }

            else if (radiosection.Checked)
            {
                try
                {
                    par1.supprimersection(param1.Text);
                    par1.chargementsection(gridparam1);                    
                    par1.chargementcombo_section_designe(combo_section_inscription);
                    par1.chargementcombo_section_designe(combo_code_section_affect_horaire);
                    par1.chargementcombocodesection(cmbSectionPrev);
                    //par1.chargementcombo_ption_cours(combo_option_cours);
                }
                catch
                {
                    XtraMessageBox.Show("Erreur de Suppression !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Choisissez une option svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void radionation_CheckedChanged_1(object sender, EventArgs e)
        {
            labelid.Text = "Code:";
            labeldesignation2.Text = "Nationalite:";
            labelid2.Text = "";
            cmbRefAdresse.Enabled = false;
            initialiserparame2();
            GridAdressage.DataSource = par1.chargement_pays();

        }

        private void radioville_CheckedChanged_1(object sender, EventArgs e)
        {
            labelid.Text = "Code:";
            labeldesignation2.Text = "Ville:";
            labelid2.Text = "Pays:";
            cmbRefAdresse.Enabled = true;
            cmbRefAdresse.Items.Clear();
            par1.chargementcombocodepays(cmbRefAdresse);
            initialiserparame2();
            GridAdressage.DataSource = par1.chargement_ville();
        }

        private void radiocom_CheckedChanged_1(object sender, EventArgs e)
        {
            labelid.Text = "Code:";
            labeldesignation2.Text = "Commune:";
            labelid2.Text = "Ville:";
            cmbRefAdresse.Enabled = true;
            cmbRefAdresse.Items.Clear();
            par1.chargementcombocodeville(cmbRefAdresse);
            initialiserparame2();
            GridAdressage.DataSource = par1.chargement_commune();
        }

        private void radioquartier_CheckedChanged_1(object sender, EventArgs e)
        {
            labelid.Text = "Code:";
            labeldesignation2.Text = "Quartier:";
            labelid2.Text = "Commune:";
            cmbRefAdresse.Enabled = true;
            cmbRefAdresse.Items.Clear();
            par1.chargementcombocodecommune(cmbRefAdresse);
            initialiserparame2();
            GridAdressage.DataSource = par1.chargement_quartier();
        }

        private void simpleButton25_Click_1(object sender, EventArgs e)
        {
            if (radionation.Checked)
            {
                try
                {
                    par1.insertionpays(txtCodeAdress.Text, txtDesigneAdress.Text);
                    el1.chargementcombonation(combonation);
                    GridAdressage.DataSource = par1.chargement_pays();
                    initialiserparame2();
                }
                catch
                {
                    XtraMessageBox.Show("Erreur de Sauvegarde !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

            }

            else if (radioville.Checked)
            {
                try
                {
                    par1.insertionville(txtCodeAdress.Text, txtDesigneAdress.Text, txtcomboRefAdresse.Text);
                    el1.chargementcomboville(comboville);
                    GridAdressage.DataSource = par1.chargement_ville();
                    initialiserparame2();
                }
                catch
                {
                    XtraMessageBox.Show("Erreur de Sauvegarde !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }

            else if (radiocom.Checked)
            {
                try
                {
                    par1.insertioncommine(txtCodeAdress.Text, txtDesigneAdress.Text, txtcomboRefAdresse.Text);
                    el1.chargementcombocommune(combocommune);
                    GridAdressage.DataSource = par1.chargement_commune();
                    initialiserparame2();
                }
                catch
                {
                    XtraMessageBox.Show("Erreur de Sauvegarde !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

            }

            else if (radioquartier.Checked)
            {
                try
                {
                    par1.insertionquartier(txtCodeAdress.Text, txtDesigneAdress.Text, txtcomboRefAdresse.Text);
                    el1.chargementcomboquartier(comboquartier);
                    GridAdressage.DataSource = par1.chargement_quartier();
                    initialiserparame2();
                }
                catch
                {
                    XtraMessageBox.Show("Erreur de Sauvegarde !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            else if(radioAvenue.Checked==true) {
                try
                {
                    par1.insertionAvenue(txtDesigneAdress.Text, txtcomboRefAdresse.Text);
                    GridAdressage.DataSource = par1.chargement_Avenue();
                    el1.chargementcomboAvenue(comboavenue);
                    initialiserparame2();
                }
                catch
                {
                    XtraMessageBox.Show("Erreur de Sauvegarde !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

            }


        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dep = e.RowIndex;
            deplacerclient(dep);
            el1.affichephotoelve(mateleve.Text, photo);
            //affichephotoelve();
        }

        private void btnprecedent_Click_1(object sender, EventArgs e)
        {
            dep++;
            deplacerclient(dep);
        }

        private void btnsuivant_Click_1(object sender, EventArgs e)
        {
            dep--;
            deplacerclient(dep);
        }

        private void rechercheeleve1_TextChanged_1(object sender, EventArgs e)
        {
            el1.rechercheeleve1(rechercheeleve1.Text, dataGridView1);
            el1.affichephotoelve(mateleve.Text, photo);
            deplacerclient(dep);
        }

        private void simpleButton7_Click_3(object sender, EventArgs e)
        {
            try
            {
                Liste_Eleve_dossiers rpt = new Liste_Eleve_dossiers();
                rpt.DataSource = clreport.GetInstance().liste_identite_tout("ViewIdentiteEleve","nom");
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btn_enregistrer_eleve_Click(object sender, EventArgs e)
        {
            try
            {

                if (mateleve.Text == "" | nom.Text == "" | postnom.Text == "" | prenom.Text == "" | combosexe.Text == "" | datenaiss.Text == "" | comboavenue.Text == "" | comboquartier.Text == "" | combocommune.Text == "" | comboville.Text == "" | combonation.Text == "" | tutaire.Text == "" | profession.Text == "" | numtutaire.Text == "" | lieu_naiss.Text == "")
                {
                    XtraMessageBox.Show("Completez tous les champs !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                {

                    el1.mergeeleve(mateleve.Text, nom.Text, postnom.Text, prenom.Text, combosexe.Text, datenaiss.Text, comboavenue.Text, comboquartier.Text, combocommune.Text, comboville.Text, combonation.Text, tutaire.Text, profession.Text, numtutaire.Text, photo.Image, lieu_naiss.Text);
                    el1.chargementeleve(dataGridView1);
                    navframe.SelectedPage = pageeleve;

                    //initialiseIdentite();

                    //deplacerclient(dep);
                }
            }
            catch
            {
                XtraMessageBox.Show("Erreur de sauvegarde !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }


        }

        void initialiseIdentite() {
            if (MessageBox.Show(this, "Voulez-vous Ajouter un eleve ?", "Innitialisation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                try
                {
                    mateleve.Text = "";
                    nom.Text = "";
                    postnom.Text = "";
                    prenom.Text = "";
                    combosexe.SelectedIndex = -1;
                    datenaiss.Text = "";
                    comboavenue.Text = "";
                    txtcomboAvenue.Text = "";
                    comboquartier.SelectedIndex = -1;
                    txtcomboQuartier.Text = "";
                    combocommune.SelectedIndex = -1;
                    txtcomboCommune.Text = "";
                    comboville.SelectedIndex = -1;
                    txtcomboVille.Text = "";
                    combonation.SelectedIndex = -1;
                    lieu_naiss.Text = "";
                    txtcomboPays.Text = "";
                    tutaire.Text = "";
                    profession.Text = ""; numtutaire.Text = "";

                    photo.Image = Image.FromFile(Environment.CurrentDirectory + @"\library\elementary_school.png");
                    //photo.Image = null;

                }
                catch
                {
                    MessageBox.Show("Erreur d'innitialiser !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }


        }

        private void btn_nouveau_eleve_Click(object sender, EventArgs e)
        {
            initialiseIdentite();
        }

        private void btn_supprimer_eleve_Click(object sender, EventArgs e)
        {
            try
            {
                if (mateleve.Text == "")
                {
                    XtraMessageBox.Show("Entrez le matricule de l'eleve a supprimer !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                {
                    el1.supprimereleve(mateleve.Text);
                    el1.chargementeleve(dataGridView1);
                }

            }
            catch
            {
                XtraMessageBox.Show("Erreur de suppression !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void btn_carte_eleve_Click(object sender, EventArgs e)
        {
            try
            {
                Carte_eleve rpt = new Carte_eleve();
                rpt.DataSource = clreport.GetInstance().Identite_eleve("Eleve", mateleve.Text);
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }
        }

        private void btn_enregistrer_inscription_Click(object sender, EventArgs e)
        {
            try
            {
                bool teste1 = ClIntelligence.GetInstance().teste_Inscription(mateleve2.Text, txtcombo_annee_inscription.Text);
                bool teste = ClIntelligence.GetInstance().teste_Option(combo_option_inscription.Text, txtcombo_section_inscription.Text);
                if (mateleve2.Text == "" | comboclasse2.Text == "" | txtcombo_annee_inscription.Text == "" | combo_division_inscription.Text == "")
                {
                    MessageBox.Show("Remplissez tous le champs svp !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                {

                    try
                    {

                        if (teste == true)
                        {
                            if (teste1 == true)
                            {
                                el1.insertioninscription_final(mateleve2.Text, comboclasse2.Text, txtcombo_option_inscription.Text, int.Parse(txtcombo_annee_inscription.Text), txtcombo_section_inscription.Text, combo_division_inscription.Text, int.Parse(txtComboEcoleUnscip.Text));
                                el1.chargementinscription(dataGridView2, txtcomboAnneeDebut.Text);
                                el1.chargementcombo_inscription(cmbElevePaie);

                                bib1.chargementcombocodelivre(combo_code_livre);
                                gridControl_recherche_inscription_emprunt.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
                                //gridControl_emprunt.DataSource = bib1.chargement_emprunt();
                                grid_historique.DataSource = el1.chargement_historique_inscription();

                                //el1.chargementcombo_affect_inscrit(combo_report_affect);
                                //el1.chargementcombo_classe_inscrite(combo_report_classe);
                                //el1.chargementcombo_option_inscrit(combo_report_option);
                                //el1.chargementcombo_section_inscrit(combo_report_section);
                                gridControl5.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
                            }
                            else {
                                MessageBox.Show("Un Eleve ne doit pas Etre Inscrit deux fois dans une meme année svp !!!","Erreur",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
                            }
                        }

                        else
                        {
                            MessageBox.Show("L'option ne correspond pas a la section svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }

                    }

                    catch (Exception)
                    {
                        XtraMessageBox.Show("Pas de lettre pour l'annee et la classe svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }


        }

        private void btn_nouveau_inscription_Click(object sender, EventArgs e)
        {
            mateleve2.Text = "";
            comboclasse2.SelectedIndex=-1;
            comboannee2.SelectedIndex=-1;
            combo_section_inscription.SelectedIndex=-1;
            combo_division_inscription.SelectedIndex=-1;
            date_inscription.Text = "";
            combo_option_inscription.SelectedIndex=-1;
            code_inscription.Text = "";
            txtcombo_annee_inscription.Text = "";
            txtcombo_option_inscription.Text = "";
            txtcombo_section_inscription.Text = "";
            cmbEcoleInscrip.SelectedIndex = -1;
            txtComboEcoleUnscip.Text = "";
            radio_co.Checked = false;
            radio_humanite.Checked = false;
            radio_final.Checked = false;
            cmbEcoleInscrip.SelectedIndex = 0;
           
            //txtComboEcoleUnscip.Text = "";

            photo1.Image = Image.FromFile(Environment.CurrentDirectory + @"\library\elementary_school.png");



        }

        private void btn_supprimer_inscription_Click(object sender, EventArgs e)
        {
            try
            {
                if (code_inscription.Text == "")
                {
                    XtraMessageBox.Show("Entrez le code de l'inscription a supprimer !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                {
                    el1.supprimereinscription(code_inscription.Text);
                    el1.chargementinscription(dataGridView2,txtcomboAnneeDebut.Text);
                    el1.chargementcombo_inscription(cmbElevePaie);
                    bib1.chargementcombocodelivre(combo_code_livre);
                    gridControl_recherche_inscription_emprunt.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
                }

            }
            catch 
            {
                XtraMessageBox.Show("Erreur de Suppression  !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }


        }

        private void simpleButton19_Click_1(object sender, EventArgs e)
        {
            if (code_inscription.Text == "")
            {
                MessageBox.Show("Selectionez l'eleve dans la liste des eleves inscrits ci-dessous !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else {
                try
                {
                    billet_inscription rpt = new billet_inscription();
                    rpt.DataSource = clreport.GetInstance().liste_inscription("billet_inscription", code_inscription.Text);
                    using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                catch (Exception ex)
                { XtraMessageBox.Show(ex.Message); }

            }

        }

        private void rechercheinscription_TextChanged_1(object sender, EventArgs e)
        {
                        
        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                el1.affichephotoelve2(mateleve2.Text, photo1);
                el1.affichephotoelve2(mateleve2.Text, photo1);
                //affichePhoto2();
                int i;
                i = dataGridView2.CurrentRow.Index;
                code_inscription.Text = dataGridView2["codeinscription", i].Value.ToString();
                mateleve2.Text = dataGridView2["codeeleve", i].Value.ToString();
                comboclasse2.Text = dataGridView2["Classe", i].Value.ToString();
                txtcombo_option_inscription.Text = dataGridView2["codeoption", i].Value.ToString();
                txtcombo_annee_inscription.Text = dataGridView2["annee", i].Value.ToString();
                txtcombo_section_inscription.Text = dataGridView2["codesection", i].Value.ToString();
                combo_division_inscription.Text = dataGridView2["division", i].Value.ToString();

                combo_section_inscription.Text = dataGridView2["section", i].Value.ToString();
                combo_option_inscription.Text = dataGridView2["optioneleve", i].Value.ToString();
                comboannee2.Text = dataGridView2["Designation_annee", i].Value.ToString();
                cmbEcoleInscrip.Text = dataGridView2["nomEcol", i].Value.ToString();
                txtComboEcoleUnscip.Text = dataGridView2["RefEcole", i].Value.ToString();
                el1.affichephotoelve2(mateleve2.Text, photo1);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnsuivant3_Click_1(object sender, EventArgs e)
        {           
        }

        private void btnprecedent3_Click_1(object sender, EventArgs e)
        {            
        }

        private void rechercheeleve2_TextChanged_1(object sender, EventArgs e)
        {
            el1.rechercheeleve2(rechercheeleve2.Text, txtRechercheInscription);
            el1.affichephotoelve2(mateleve2.Text, photo1);
        }

        private void dataGridView3_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                int i;
                i = txtRechercheInscription.CurrentRow.Index;                
                mateleve2.Text = txtRechercheInscription[0, i].Value.ToString();
                el1.affichephotoelve2(mateleve2.Text, photo1);
            }
            catch
            {

            }            
        }

        private void btnsuivant2_Click_1(object sender, EventArgs e)
        {           
        }

        private void btnprecedent2_Click_1(object sender, EventArgs e)
        {            
        }

        private void simpleButton80_Click_1(object sender, EventArgs e)
        {           
        }

        private void radio_raprt_co_CheckedChanged_1(object sender, EventArgs e)
        {
        }

        private void radio_raport_moyen_CheckedChanged_1(object sender, EventArgs e)
        {
        }

        private void radio_raport_final_CheckedChanged_1(object sender, EventArgs e)
        {
        }

        private void combo_report_classe_SelectedIndexChanged_1(object sender, EventArgs e)
        {
        }

        private void comboclasse2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                int choix = int.Parse(comboclasse2.Text);
                if (choix == 1 | choix == 2)
                {
                    radio_co.Checked = true;
                }

                else if (choix == 3 | choix == 4)
                {
                    radio_humanite.Checked = true;
                }
                else if (choix == 5 | choix == 6)
                {
                    radio_final.Checked = true;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

        }

        private void radio_co_CheckedChanged_1(object sender, EventArgs e)
        {
            combo_option_inscription.Enabled = true;
            combo_section_inscription.Enabled = true;
        }

        private void radio_humanite_CheckedChanged_1(object sender, EventArgs e)
        {
            combo_option_inscription.Enabled = true;
            combo_section_inscription.Enabled = true;
        }

        private void radio_final_CheckedChanged_1(object sender, EventArgs e)
        {
            combo_option_inscription.Enabled = true;
            combo_section_inscription.Enabled = true;
        }

        private void simpleButton38_Click_2(object sender, EventArgs e)
        {
            codeperiode.Text = "";
            periode.Text = "";
        }

        private void simpleButton39_Click_2(object sender, EventArgs e)
        {
            if (codeperiode.Text == "" | periode.Text == "")
            {

                XtraMessageBox.Show("Completez tous les champs svp !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            else
            {
                try
                {
                    if (IsNumeric(codeperiode.Text))
                    {

                        bool teste = ClIntelligence.GetInstance().teste_periode(codeperiode.Text);

                        if (teste == true)
                        {
                            cr1.insertionperiode(int.Parse(codeperiode.Text), periode.Text);
                            codeperiode.Text = "";
                            periode.Text = "";
                            cr1.chargementcombo_periode_designe(combocode_periode_affect);
                            cr1.chargementcombo_periode_pourcent(combo_periode_pourcent);

                            cr1.chargementperiode(gridperiode);

                            cr1.chargementcombo_periode_designe(comboperiode);
                        }
                        else {
                            MessageBox.Show("Le code existe deja svp !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        
                    }
                    else
                    {
                        XtraMessageBox.Show("Pas de lettre pour le code de la periode !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }


                }
                catch 
                {
                    XtraMessageBox.Show("Pas de lettre pour le code de la periode !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }



            }

        }

        private void simpleButton40_Click_2(object sender, EventArgs e)
        {
            try
            {

                cr1.modifierperiode(periode.Text, codeperiode.Text);
                cr1.chargementperiode(gridperiode);
                cr1.chargementcombo_periode_pourcent(combo_periode_pourcent);

                cr1.chargementcombo_periode_designe(combocode_periode_affect);

                cr1.chargementcombo_periode_designe(comboperiode);
                codeperiode.Text = "";
                periode.Text = "";
            }

            catch
            {
                XtraMessageBox.Show("Erreur de modification !!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void simpleButton41_Click_2(object sender, EventArgs e)
        {
            try
            {
                cr1.supprimerperiode(codeperiode.Text);
                cr1.chargementperiode(gridperiode);
                cr1.chargementcombo_periode_designe(combocode_periode_affect);
                cr1.chargementcombo_periode_pourcent(combo_periode_pourcent);
                cr1.chargementcombo_periode_designe(comboperiode);
                codeperiode.Text = "";
                periode.Text = "";
            }
            catch 
            {
                XtraMessageBox.Show("Erreur de suppression !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void rechercheperiode_TextChanged(object sender, EventArgs e)
        {
            cr1.rechercheperiode(rechercheperiode.Text, gridperiode);
            deplacerperiode(dep);
        }

        private void gridperiode_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dep = e.RowIndex;
            deplacerperiode(dep);
        }

        private void btnsuivant7_Click(object sender, EventArgs e)
        {
            dep++;
            deplacerperiode(dep);
        }

        private void btnprecedent7_Click(object sender, EventArgs e)
        {
            dep--;
            deplacerperiode(dep);
        }

        private void gridcours_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dep = e.RowIndex;
            deplacerecours(dep);
        }

        private void simpleButton44_Click_1(object sender, EventArgs e)
        {
            try
            {
                cr1.modifiercours(cours.Text, codecours.Text);
                cr1.chargementcours(gridcours);
                cr1.chargementcombocours(combocours);
                cr1.chargementcombocours(cours_affect);
                cr1.chargementcombocours(combo_cours_affect_horaire);                
                cr1.chargementcombocours(cours_affect);
                //codecours.Text = "";
                //cours.Text = "";

            }
            catch 
            {
                XtraMessageBox.Show("Erreur de modification !!!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }

        }

        private void simpleButton34_Click_2(object sender, EventArgs e)
        {
            try
            {
                cr1.insertioncours(cours.Text);
                cr1.chargementcombocours(cours_affect);
                cr1.chargementcours(gridcours);
                cr1.chargementcombocours(combocours);
                cr1.chargementcombocours(combo_cours_affect_horaire);                
                cr1.chargementcombocours(cours_affect);
            }
            catch 
            {
                XtraMessageBox.Show("Erreur de Sauvegarde !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }


        }

        private void simpleButton35_Click_2(object sender, EventArgs e)
        {
            codecours.Text = "";
            cours.Text = "";
            codecours.Enabled = false;
        }

        private void simpleButton36_Click(object sender, EventArgs e)
        {
            try
            {
                cr1.supprimercours(codecours.Text);
                gridControl12.DataSource = cr1.chargement_proclamation();
                cr1.chargementcours(gridcours);
                cr1.chargementcombocours(cours_affect);
                cr1.chargementcombocours(combocours);
                
                

            }
            catch 
            {
                XtraMessageBox.Show("Erreur de Suppession  !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void recherchecours_TextChanged_1(object sender, EventArgs e)
        {
            cr1.recherchecours(recherchecours.Text, gridcours);
            deplacerecours(dep);
        }

        private void btnprecedent6_Click_1(object sender, EventArgs e)
        {
            dep--;
            deplacerecours(dep);
        }

        private void btnsuivant6_Click_1(object sender, EventArgs e)
        {
            dep++;
            deplacerecours(dep);
        }

        private void gridcours_Click(object sender, EventArgs e)
        {            
        }

        private void combo_classe_cours_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void radio_co_cours_CheckedChanged_1(object sender, EventArgs e)
        {            
        }

        private void radio_moyen_cours_CheckedChanged_1(object sender, EventArgs e)
        {          
            
        }

        private void radio_final_cours_CheckedChanged_1(object sender, EventArgs e)
        {
        }

        private void simpleButton37_Click(object sender, EventArgs e)
        {
            
        }

        private void classe_affect_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int choix = int.Parse(classe_affect.Text);
            if (choix == 1 | choix == 2)
            {
                radio_co_affect.Checked = true;
            }

            else if (choix == 3 | choix == 4)
            {
                radio_moyen_affect.Checked = true;
            }

            else if (choix == 5 | choix == 6)
            {
                radio_final_affect.Checked = true;
            }
        }

        private void radio_co_affect_CheckedChanged_1(object sender, EventArgs e)
        {
            combocode_option_affect.Enabled = true;
            combo_section_affect.Enabled = true;
        }

        private void radio_moyen_affect_CheckedChanged_1(object sender, EventArgs e)
        {
            combocode_option_affect.Enabled = true;
            combo_section_affect.Enabled = true;
        }

        private void radio_final_affect_CheckedChanged_1(object sender, EventArgs e)
        {
            combocode_option_affect.Enabled = true;
            combo_section_affect.Enabled = true;
        }

        private void simpleButton57_Click_1(object sender, EventArgs e)
        {
            initialiserAffectation();
        }

        private void btn_enregistrer_affectation_Click(object sender, EventArgs e)
        {

            try
            {
                bool teste = ClIntelligence.GetInstance().teste_Option(combocode_option_affect.Text, txtcombo_section_affect.Text);
                bool teste1 = ClIntelligence.GetInstance().teste_Affectation(classe_affect.Text, txtcombo_section_affect.Text, txtcombo_option_affect.Text, txtcombo_annee_affect.Text, cours_affect.Text, txtcombo_periode_affect.Text);
                bool teste3 = ClIntelligence.GetInstance().teste_Periode(txtcombo_annee_affect.Text, classe_affect.Text, txtcombo_periode_affect.Text);
                if (max_affect.Text == "" | cours_affect.Text == "" | txtcombo_annee_affect.Text == "" | txtcombo_ens_affect.Text == "" | classe_affect.Text == "" | txtcombo_periode_affect.Text == "")
                {
                    XtraMessageBox.Show("Completez tous les champs svp !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        if (IsNumeric(max_affect.Text) | IsNumeric(annee_affect.Text))
                        {
                            if (double.Parse(max_affect.Text) > 0)
                            {
                                if (teste == true)
                                {
                                    if (teste3 == true)
                                    {
                                        if (teste1 == true)
                                        {
                                            cr1.insertionaffectation_final(double.Parse(max_affect.Text), cours_affect.Text, int.Parse(txtcombo_annee_affect.Text), txtcombo_ens_affect.Text, classe_affect.Text, txtcombo_section_affect.Text, txtcombo_option_affect.Text, txtcombo_periode_affect.Text, int.Parse(txtcomboEcoleAffect.Text));
                                            cr1.chargementaffectation(gridaffectation, txtcomboAnneeDebut.Text);
                                            gridControl12.DataSource = cr1.chargement_proclamation();
                                        }
                                        else {
                                            MessageBox.Show("Cette attribution est deja faite svp !!!", "Une Attribution 2 Fois", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("La période precedente n'est pas encore clôturée pour la classe choisie svp !!! veillez contacter l'administrateur !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("L'option ne correspond pas a la section !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                }

                            }
                            else
                            {
                                MessageBox.Show("Le Maximum doit etre superieur a 0 svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }

                        }

                        else
                        {

                            XtraMessageBox.Show("Pas de lettre svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }

                    }
                    catch
                    {
                        XtraMessageBox.Show("Pas de lettre !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }           
        }

        private void supprimer_affectation_Click(object sender, EventArgs e)
        {
            try
            {
                if (codeaffect.Text == "")
                {

                    XtraMessageBox.Show("Completer le code de l'affectation a supprimer !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                {
                    cr1.supprimeraffectation(codeaffect.Text);
                    gridControl12.DataSource = cr1.chargement_proclamation();
                    cr1.chargementaffectation(gridaffectation,txtcomboAnneeDebut.Text);
                }


            }
            catch 
            {
                XtraMessageBox.Show("Erreur de Suppression !!!!1", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }


        }

        private void rechercheaffect_TextChanged_1(object sender, EventArgs e)
        {
            cr1.rechercheaffectation(rechercheaffect.Text, gridaffectation,UserSession.GetInstance().Annee);
        }

        private void gridaffectation_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dep = e.RowIndex;
            deplacerAffectation(dep);
        }

        private void btnprecedent10_Click_1(object sender, EventArgs e)
        {
            dep--;
            deplacerAffectation(dep);
        }

        private void btnsuivant10_Click_1(object sender, EventArgs e)
        {
            dep++;
            deplacerAffectation(dep);
        }

        private void simpleButton45_Click_1(object sender, EventArgs e)
        {
            nouvelle_cotation();
        }

        private void simpleButton47_Click_1(object sender, EventArgs e)
        {
            if (Tempannee != "" && Tempclasse != "" && TempOption != "" && TempSection != "")
            {
                double max = ClIntelligence.GetInstance().teste_Maximum(Tempclasse, TempSection, TempOption, Tempannee, combocours.Text, txtcombo_periode_cote.Text);
                bool teste1 = ClIntelligence.GetInstance().teste_Cotation(combocours.Text, txtcombo_periode_cote.Text, comboeleve.Text);
                bool teste2 = ClIntelligence.GetInstance().teste_Periode(Tempannee, Tempclasse, txtcombo_periode_cote.Text);
                bool teste3 = ClIntelligence.GetInstance().teste_Cours(Tempannee, Tempclasse, TempSection, TempOption, combocours.Text);
                try
                {

                    if (double.Parse(cote.Text) < 0)
                    {
                        MessageBox.Show("La cote ne doit pas etre negative !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else
                    {

                        if (double.Parse(cote.Text) <= max)
                        {

                            if (teste1 == true)
                            {
                                if (teste2 == true)
                                {

                                    if (teste3 == true)
                                    {
                                        cr1.insertioncotation(double.Parse(cote.Text), int.Parse(txtcombo_periode_cote.Text), combocours.Text, int.Parse(comboeleve.Text));
                                        cr1.chargementcotation(gridcotation, txtcomboAnneeDebut.Text);
                                        //cr1.chargement_code_eleve_cote(combo_ele);
                                        gridControl12.DataSource = cr1.chargement_proclamation();

                                        Tempannee = "";
                                        Tempclasse = "";
                                        TempOption = "";
                                        TempSection = "";
                                    }
                                    else
                                    {
                                        MessageBox.Show("Erreur de cotation !!! car le cours sélectionné ne concerne pas la classe, la section ni l’option de l’élève svp !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("La période precedente n'est pas encore clôturée pour la classe choisie svp !!! veillez contacter l'administrateur !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                }

                            }
                            else
                            {
                                MessageBox.Show("L'eleve a deja une cote dans ce cours ert la meme periode !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }

                        }
                        else
                        {
                            MessageBox.Show("La cote est invalide svp !!! ca depasse le maximum !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }


                    }
                    //if (IsNumeric(cote.Text))
                    //{

                    //}
                    //else
                    //{
                    //    MessageBox.Show("La cote doit etre en numeric svp !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    //}

                    //nouvelle_cotation();
                }

                catch
                {
                    XtraMessageBox.Show("Erreur de Sauvegarde !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }



            }
            else {
                MessageBox.Show("Sélectionné d’abord l’élève dans liste ci-haut svp !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }           

        }

        private void simpleButton48_Click_1(object sender, EventArgs e)
        {
            //bool teste = ClIntelligence.GetInstance().teste_Maximum(Tempclasse, TempSection, TempOption, Tempannee, combocours.Text, txtcombo_periode_cote.Text, cote.Text);
            //bool teste1 = ClIntelligence.GetInstance().teste_Cotation(Tempclasse, TempSection, TempOption, Tempannee, combocours.Text, txtcombo_periode_cote.Text,comboeleve.Text);
            
            try
            {
                if (IsNumeric(cote.Text))
                {
                    if (double.Parse(cote.Text) < 0)
                    {
                        MessageBox.Show("La doit superieur ou egale a 0 !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else {
                        cr1.modifiercotation(codecote.Text, double.Parse(cote.Text), int.Parse(txtcombo_periode_cote.Text), combocours.Text, int.Parse(comboeleve.Text));
                        cr1.chargementcotation(gridcotation, txtcomboAnneeDebut.Text);
                        //cr1.chargement_code_eleve_cote(combo_ele);
                        gridControl12.DataSource = cr1.chargement_proclamation();
                        //if (teste == true)
                        //{


                        //}
                        //else {
                        //    MessageBox.Show("La cote est invalide svp !!!! Elle est superieur au maximum !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        //}

                    }
                    
                }
                else {
                    MessageBox.Show("La cote doit etre en numerique svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                
                //nouvelle_cotation();
            }
            catch 
            {
                XtraMessageBox.Show("Erreur de Modification !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void simpleButton49_Click_1(object sender, EventArgs e)
        {
            try
            {
                cr1.supprimercotation(codecote.Text);
                gridControl12.DataSource = cr1.chargement_proclamation();
                cr1.chargementcotation(gridcotation,txtcomboAnneeDebut.Text);
                //cr1.chargement_code_eleve_cote(combo_ele);
                //nouvelle_cotation();
            }
            catch
            {
                XtraMessageBox.Show("Erreur de suppression !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void recherchecote_TextChanged_1(object sender, EventArgs e)
        {
            cr1.recherchecotation(recherchecote.Text, gridcotation,UserSession.GetInstance().Annee);
        }

        private void btnprecedent8_Click_1(object sender, EventArgs e)
        {
            dep--;
            deplacercotation(dep);
        }

        private void btnsuivant8_Click_1(object sender, EventArgs e)
        {
            dep++;
            deplacercotation(dep);
        }

        private void gridcotation_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dep = e.RowIndex;
            deplacercotation(dep);
        }

        private void gridControl5_MouseClick_1(object sender, MouseEventArgs e)
        {
            try
            {
                comboeleve.Text = gridView5.GetFocusedRowCellValue("codeinscription").ToString();

                Tempannee = gridView5.GetFocusedRowCellValue("annee").ToString();
                Tempclasse = gridView5.GetFocusedRowCellValue("Classe").ToString();
                TempOption = gridView5.GetFocusedRowCellValue("codeoption").ToString();
                TempSection = gridView5.GetFocusedRowCellValue("codesection").ToString();


                combocours.Items.Clear();
                ClIntelligence.GetInstance().chargementcombocours(combocours, Tempannee, Tempclasse, TempOption, TempSection);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btn_nouveau_paiement_Click(object sender, EventArgs e)
        {
            initialiser_paie();
        }

        
        
        void EnvoiMessage() {

            try
            {
                string[] tab = new string[4];
                for (int i = 0; i <= 3; i++)
                {
                    tab[i] = ClIntelligence.GetInstance().RetourneEleve(cmbElevePaie.Text)[i];
                }
                string contenu = "Bonjour cher parent Votre Enfant " + tab[0] + " " + tab[1] + " " + tab[2] + " a payé : " + txtmontantpay.Text + "$ a la date " + txtdatepay.Text + ", au pres du comptable "+ LabelUser.Text+ ". Administration         ";
                string numero = tab[3];

                DialogResult res = MessageBox.Show("voulez vous envoyer des messages de rapport depaiement de l'eveve au tutaire a la date " + DateTime.Now + "?", "Envoi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    ClsMessagerieInsert m = new ClsMessagerieInsert();
                    message.sendlongMsg(numero, contenu);
                    m.Numero1 = numero;
                    m.MessateTexte1 = contenu;
                    m.EtatSms1 = 0;
                    m.DateEvoie1 = DateTime.Parse(txtdatepay.Text);
                    m.Utilisateur1 = LabelUser.Text;
                    ClIntelligence.GetInstance().insertMessagerie(m);
                }
                else
                {
                    MessageBox.Show("vous n'avez pas envoyé!");
                    ClsMessagerieInsert m = new ClsMessagerieInsert();
                    m.Numero1 = numero;
                    m.MessateTexte1 = contenu;
                    m.EtatSms1 = 0;
                    m.DateEvoie1 = DateTime.Parse(txtdatepay.Text);
                    m.Utilisateur1 = LabelUser.Text;
                    ClIntelligence.GetInstance().insertMessagerie(m);
                }                

                
                
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void btn_enregistrer_paiement_Click(object sender, EventArgs e)
        {
            bool teste1 = ClIntelligence.GetInstance().teste_Paiement1(cmbElevePaie.Text);
            bool teste2 = ClIntelligence.GetInstance().teste_PaiementExist(Tempclasse, TempSection, TempOption, Tempannee, txtcomboTypeFrais.Text, cmbElevePaie.Text, txtmontantpay.Text);
            bool teste3 = ClIntelligence.GetInstance().teste_PaiementNotExist(Tempclasse, TempSection, TempOption, Tempannee, txtcomboTypeFrais.Text, cmbElevePaie.Text, txtmontantpay.Text);

            try
            {
                if (cmbElevePaie.Text == "" | txtcomboTypeFrais.Text == "" | txtmontantpay.Text == "")
                {
                    MessageBox.Show("Remplissez tous le champs svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                { 
                    if (double.Parse(txtmontantpay.Text) < 0)
                    {
                        MessageBox.Show("Le Montant ne doit pas etre inferieur a 0 !!!");
                    }
                    else {
                        el1.insertion_paiement(double.Parse(txtmontantpay.Text), int.Parse(cmbElevePaie.Text), txtcomboTypeFrais.Text, LabelUser.Text, "Frais "+cmbtypefrais.Text);
                        el1.chargementpaiement(gridpaiement,txtcomboAnneeDebut.Text);
                        gridControl3.DataSource = ens.chargement_solde_caisse();
                        grid_historique.DataSource = el1.chargement_historique_paiement();


                        DialogResult res = MessageBox.Show("voulez vous envoyer des messages de rapport depaiement de l'eveve au tutaire " + DateTime.Now + "?", "Envoi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {
                            EnvoiMessage();
                        }
                        else
                        {
                            MessageBox.Show("vous n'avez pas envoyé!");

                        }

                        
                    }


                }


            }
            catch 
            {
                XtraMessageBox.Show("Erreur de Sauvegarde !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }


        }

        private void btn_imprimer_recus_Click(object sender, EventArgs e)
        {
            try
            {
                Recus_paiement_Final rpt = new Recus_paiement_Final();
                rpt.DataSource = clreport.GetInstance().liste_paiement("view_paiement_vrai", txtcodePaiement.Text);
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }
        }

        private void combotypefrais_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            el1.sairie_code_frais(txtcomboTypeFrais, cmbtypefrais.Text);
        }

        private void comboutilisateur_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //ens.saisir_code_comptable(txtcomboComptablePaie, comboutilisateur.Text);
        }

        private void recherchepaiement_TabStopChanged(object sender, EventArgs e)
        {
           
        }

        private void gridpaiement_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {            
        }

        private void btnsuivant5_Click(object sender, EventArgs e)
        {
           
        }

        private void btnprecedent5_Click_1(object sender, EventArgs e)
        {           
        }

        private void gridrecherchepaiement_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void rechercheeleve_paie_TextChanged_1(object sender, EventArgs e)
        {            
        }

        private void btnsuivant4_Click_1(object sender, EventArgs e)
        {           
        }

        private void btnprecedent4_Click_1(object sender, EventArgs e)
        {            
        }

        private void recherchepaiement_TextChanged_1(object sender, EventArgs e)
        {           
        }

        private void btn_nouveau_livre_Click(object sender, EventArgs e)
        {
            initialiser_livre();
        }

        private void btn_enregistrer_livre_Click(object sender, EventArgs e)
        {
            try
            {
                bib1.insertion_livre(codelivre.Text, titre_livre.Text, auteur_livre.Text, etat_livre.Text, double.Parse(nombre_livre.Text));

                gridControl6.DataSource = bib1.chargement_livre();
                bib1.chargementcombocodelivre(combo_code_livre);
                //gridControl_recherche_inscription.DataSource = el1.chargement_recherche_inscription();
                gridControl_recherche_inscription_emprunt.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
                
            }

            catch 
            {
                XtraMessageBox.Show("Erreur d'Enregistrement !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }


        }

        private void btn_supprimer_livre_Click(object sender, EventArgs e)
        {
            try
            {
                bib1.supprimer_livre(codelivre.Text);
                bib1.chargementcombocodelivre(combo_code_livre);
                gridControl6.DataSource = bib1.chargement_livre();
                gridControl_recherche_inscription_emprunt.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
               
            }
            catch 
            {
                XtraMessageBox.Show("Erreur de suppression !!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void gridControl6_MouseClick_1(object sender, MouseEventArgs e)
        {
            try
            {
                codelivre.Text = gridView6.GetFocusedRowCellValue("code_livre").ToString();
                titre_livre.Text = gridView6.GetFocusedRowCellValue("titre_livre").ToString();
                auteur_livre.Text = gridView6.GetFocusedRowCellValue("auteur").ToString();
                etat_livre.Text = gridView6.GetFocusedRowCellValue("etat_livre").ToString();
                nombre_livre.Text = gridView6.GetFocusedRowCellValue("nombre_livre").ToString();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btn_nouvel_emprunt_Click(object sender, EventArgs e)
        {
            code_emprunt.Text = "";
            combo_inscription_emprunt.Text = "";
            combo_code_livre.Text = "";
            date_emprunt.Text = "";
            date_retour.Text = "";
            txtnombreLivreEmprunt.Text = "";
            txtnomEleveEmprunt.Text = "";
        }

        private void btn_enregistrer_emprunt_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (combo_inscription_emprunt.Text == "" | txt_combo_livre.Text == "" | date_emprunt.Text == "" | date_retour.Text == "" | txtnombreLivreEmprunt.Text == "")
                {
                    MessageBox.Show("Completez tous les champs svp !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else {
                    bool teste = ClIntelligence.GetInstance().teste_EmpruntLivre(txt_combo_livre.Text, txtnombreLivreEmprunt.Text);

                    if (teste == true)
                    {
                        bib1.insertion_emprunt(int.Parse(combo_inscription_emprunt.Text), txt_combo_livre.Text, DateTime.Parse(date_emprunt.Text), DateTime.Parse(date_retour.Text), int.Parse(txtnombreLivreEmprunt.Text));
                        gridControl9.DataSource = bib1.chargement_emprunt(txtcomboAnneeDebut.Text);
                        gridControl10.DataSource = bib1.chargement_recherche_emprunt_livre(txtcomboAnneeDebut.Text);
                        gridControl6.DataSource = bib1.chargement_livre();
                    }
                    else {
                        MessageBox.Show("Le nombre de Livre depasse le stock sponible svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    
                }             

               
            }
            catch 
            {
                XtraMessageBox.Show("Erreur de sauvegarde !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }

        }

        private void btn_supprimer_emprunt_Click(object sender, EventArgs e)
        {
            try
            {
                bib1.supprimer_emprunt(code_emprunt.Text);
                gridControl9.DataSource = bib1.chargement_emprunt(txtcomboAnneeDebut.Text);
                gridControl10.DataSource = bib1.chargement_recherche_emprunt_livre(txtcomboAnneeDebut.Text);
                gridControl6.DataSource = bib1.chargement_livre();
            }
            catch 
            {
                XtraMessageBox.Show("Erreur de suppression !!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void simpleButton109_Click_1(object sender, EventArgs e)
        {
            try
            {
                Carte_bibliotheque rpt = new Carte_bibliotheque();
                rpt.DataSource = clreport.GetInstance().liste_bibliotheque("emprunt_bibliotheque", int.Parse(code_emprunt.Text));
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }
        }

        private void gridControl_recherche_inscription_emprunt_MouseClick_1(object sender, MouseEventArgs e)
        {
            try
            {
                combo_inscription_emprunt.Text = gridView_recherche_inscription_emprunt.GetFocusedRowCellValue("codeinscription").ToString();
                txtnomEleveEmprunt.Text = gridView_recherche_inscription_emprunt.GetFocusedRowCellValue("nom").ToString();

            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void gridControl9_MouseClick_1(object sender, MouseEventArgs e)
        {
            try
            {
                code_emprunt.Text = gridView9.GetFocusedRowCellValue("num").ToString(); ;
                combo_inscription_emprunt.Text = gridView9.GetFocusedRowCellValue("ref_inscription").ToString(); ;
                combo_code_livre.Text = gridView9.GetFocusedRowCellValue("ref_livre").ToString();
                date_emprunt.Text = gridView9.GetFocusedRowCellValue("date_retrait").ToString();
                date_retour.Text = gridView9.GetFocusedRowCellValue("date_retour").ToString();
                txtnombreLivreEmprunt.Text = gridView9.GetFocusedRowCellValue("Nombre").ToString();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btn_nouvel_remise_Click(object sender, EventArgs e)
        {
            code_remise.Text = "";
            combo_code_emprunt.Text = "";
            confirme_remise.Text = "";
            txtnomEleveRetourLivre.Text = "";
            txtcomboLivreRetour.Text = "";
            cmbLivreRetour.Text = "";
        }

        private void btn_enregistrer_remise_Click(object sender, EventArgs e)
        {
            try
            {
                if (combo_code_emprunt.Text == "" | confirme_remise.Text == "" | txtcomboLivreRetour.Text == "")
                {
                    MessageBox.Show("Completez tous champs svp !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else {
                    bool teste = ClIntelligence.GetInstance().teste_RemiseLivre(combo_code_emprunt.Text, confirme_remise.Text,txtcomboLivreRetour.Text);
                    if (teste == true)
                    {
                        bib1.insertion_remise(int.Parse(combo_code_emprunt.Text), int.Parse(confirme_remise.Text), txtcomboLivreRetour.Text);
                        gridControl11.DataSource = bib1.chargement_remise_livre(txtcomboAnneeDebut.Text);
                        gridControl6.DataSource = bib1.chargement_livre();
                    }
                    else {
                        MessageBox.Show("Les nombre de livre et le livre remis sont invalide a cette emprunt !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    
                }
                

            }
            catch 
            {
                XtraMessageBox.Show("Erreur de Sauvegarde !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void btn_supprimer_remise_Click(object sender, EventArgs e)
        {
            try
            {
                bib1.supprimer_remise_livre(code_remise.Text);
                gridControl11.DataSource = bib1.chargement_remise_livre(txtcomboAnneeDebut.Text);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show("Erreur de suppression !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void gridControl10_MouseClick_1(object sender, MouseEventArgs e)
        {
            try
            {
                combo_code_emprunt.Text = gridView10.GetFocusedRowCellValue("num").ToString();
                cmbLivreRetour.Text = gridView10.GetFocusedRowCellValue("titre_livre").ToString();
                txtcomboLivreRetour.Text = gridView10.GetFocusedRowCellValue("code_livre").ToString();
                txtnomEleveRetourLivre.Text = gridView10.GetFocusedRowCellValue("nom").ToString();

            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void gridControl11_MouseClick_1(object sender, MouseEventArgs e)
        {

            try
            {
                code_remise.Text = gridView11.GetFocusedRowCellValue("code_retour").ToString();
                combo_code_emprunt.Text = gridView11.GetFocusedRowCellValue("ref_emprunt").ToString();
                confirme_remise.Text = gridView11.GetFocusedRowCellValue("NombreR").ToString();
                cmbLivreRetour.Text= gridView11.GetFocusedRowCellValue("titre_livre").ToString();
                txtnomEleveRetourLivre.Text = gridView11.GetFocusedRowCellValue("nom").ToString();
                txtcomboLivreRetour.Text = gridView11.GetFocusedRowCellValue("ref_livre").ToString();

            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void combo_classe_affect_horaire_SelectedIndexChanged_1(object sender, EventArgs e)
        {          

        }

        private void radio_co_horaire_CheckedChanged_1(object sender, EventArgs e)
        {            
        }

        private void radio_moyen_horaire_CheckedChanged_1(object sender, EventArgs e)
        {            
        }

        private void radio_final_horaire_CheckedChanged_1(object sender, EventArgs e)
        {            
        }

        private void simpleButton71_Click_1(object sender, EventArgs e)
        {
            try
            {
                code_affect_horaire.Text = "";
                combo_classe_affect_horaire.SelectedIndex = -1;
                txtcombo_option_horaire.Text = "";
                combo_option_affect_horaire.SelectedIndex = -1;

                txtcombo_jours_horaire.Text = "";
                comb_jours_affect_horaire.SelectedIndex = -1;

                combo_cours_affect_horaire.SelectedIndex = -1;
                heure_debut.Text = "";
                heure_fin.Text = "";

                txtcombo_ens_horaire.Text = "";
                combo_ens_affect_horaire.SelectedIndex = -1;

                txtcombo_annee_horaire.Text = "";
                combo_anne_affect_horaire.SelectedIndex = -1;

                txtcombo_section_horaire.Text = "";
                combo_code_section_affect_horaire.SelectedIndex = -1;

                combo_division_horaire.Text = "";


                // affichephotoEns();

            }
            catch
            {

            }
        }

        private void simpleButton73_Click_1(object sender, EventArgs e)
        {
            if (combo_classe_affect_horaire.Text == "" | combo_division_horaire.Text == "" | txtcombo_jours_horaire.Text == "" | combo_cours_affect_horaire.Text == "" | heure_debut.Text == "" | heure_fin.Text == "" | txtcombo_ens_horaire.Text == "" | txtcombo_annee_horaire.Text == "")
            {
                XtraMessageBox.Show("Completez tous les champs svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            else
            {
                bool teste = ClIntelligence.GetInstance().teste_AffectationHoraire(combo_classe_affect_horaire.Text, txtcombo_section_horaire.Text, txtcombo_option_horaire.Text, txtcombo_annee_horaire.Text, combo_division_horaire.Text, combo_cours_affect_horaire.Text, txtcombo_jours_horaire.Text,heure_debut.Text,heure_fin.Text);
                try
                {

                    if (teste == true)
                    {
                        hor1.insertion_horaire_final1(combo_classe_affect_horaire.Text, txtcombo_option_horaire.Text, int.Parse(txtcombo_jours_horaire.Text), combo_cours_affect_horaire.Text, heure_debut.Text, heure_fin.Text, txtcombo_ens_horaire.Text, int.Parse(txtcombo_annee_horaire.Text), txtcombo_section_horaire.Text, combo_division_horaire.Text,int.Parse(txtcomboEcoleHoraire.Text));
                        hor1.chargement_horaire(gridhoraire, txtcomboAnneeDebut.Text);
                    }
                    else
                    {
                        MessageBox.Show("Cette meme affectation existe deja svp !!!", "Une affectaion 2 fois", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Erreur de Sauvegarde !!!!!" + ex.Message);
                }



            }

        }

        private void simpleButton72_Click_1(object sender, EventArgs e)
        {
            if (code_affect_horaire.Text == "" | combo_classe_affect_horaire.Text == "" | combo_division_horaire.Text == "" | txtcombo_jours_horaire.Text == "" | combo_cours_affect_horaire.Text == "" | heure_debut.Text == "" | heure_fin.Text == "" | txtcombo_ens_horaire.Text == "" | txtcombo_annee_horaire.Text == "")
            {
                XtraMessageBox.Show("Completez tous les champs svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }


            else
            {
                bool teste = ClIntelligence.GetInstance().teste_AffectationHoraire(combo_classe_affect_horaire.Text, txtcombo_section_horaire.Text, txtcombo_option_horaire.Text, txtcombo_annee_horaire.Text, combo_division_horaire.Text, combo_cours_affect_horaire.Text, txtcombo_jours_horaire.Text,heure_debut.Text, heure_fin.Text);
                try
                {

                    if (teste == true)
                    {
                        hor1.insertion_horaire_final(int.Parse(code_affect_horaire.Text), combo_classe_affect_horaire.Text, txtcombo_option_horaire.Text, int.Parse(txtcombo_jours_horaire.Text), combo_cours_affect_horaire.Text, heure_debut.Text, heure_fin.Text, txtcombo_ens_horaire.Text, int.Parse(txtcombo_annee_horaire.Text), txtcombo_section_horaire.Text, combo_division_horaire.Text,int.Parse(txtcomboEcoleHoraire.Text));
                        hor1.chargement_horaire(gridhoraire, txtcomboAnneeDebut.Text);
                    }
                    else
                    {
                        MessageBox.Show("Cette meme affectation existe deja svp !!!", "Une affectaion 2 fois", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Erreur de Sauvegarde !!!!!" + ex.Message);
                }



            }
        }

        private void simpleButton6_Click_1(object sender, EventArgs e)
        {
            if (code_affect_horaire.Text == "")
            {
                XtraMessageBox.Show("Entrez le code l'affectation de l'horaire a supprimer !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            else
            {
                try
                {
                    hor1.supprimer_horaire(code_affect_horaire.Text);
                    hor1.chargement_horaire(gridhoraire,txtcomboAnneeDebut.Text);
                    hor1.chargementcombo_jours_designe(comb_jours_affect_horaire);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Erreur de Suppression  !!!!!" + ex.Message);
                }
            }


        }

        private void recherchehoraire_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void gridhoraire_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {           
        }

        private void btnprecedent12_Click_1(object sender, EventArgs e)
        {            
        }

        private void btnsuivant12_Click_1(object sender, EventArgs e)
        {            
        }

        private void simpleButton69_Click_1(object sender, EventArgs e)
        {
            JOURS.Text = "";
            code_jours.Text = "";
        }

        private void simpleButton70_Click_1(object sender, EventArgs e)
        {
            try
            {
                hor1.insertion_affectation_horaire(int.Parse(code_jours.Text), JOURS.Text);
                hor1.chargement_affectation_horaire(grid_affectation_horaire);
                hor1.chargementcombo_jours_designe(comb_jours_affect_horaire);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur d'enregistrement !!!!!" + ex.Message);
            }
        }

        private void simpleButton75_Click_1(object sender, EventArgs e)
        {
            try
            {
                hor1.modifier_affectation_horaire(int.Parse(code_jours.Text), int.Parse(JOURS.Text));
                hor1.chargement_affectation_horaire(grid_affectation_horaire);
                hor1.chargementcombo_jours_designe(comb_jours_affect_horaire);
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show("Erreur de modification !!!!!" + ex.Message);
            }
        }

        private void simpleButton76_Click_1(object sender, EventArgs e)
        {
            try
            {

                hor1.supprimer_affectation_horaire(code_jours.Text);
                hor1.chargement_affectation_horaire(grid_affectation_horaire);
                hor1.chargementcombo_jours_designe(comb_jours_affect_horaire);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Erreur de suppression !!!!!" + ex.Message);

            }
        }

        private void recherche_affect_recherche_TextChanged_1(object sender, EventArgs e)
        {
            hor1.recherche_affectation_horaire(recherche_affect_recherche.Text, grid_affectation_horaire);
        }

        private void label128_Click(object sender, EventArgs e)
        {

        }

        private void label81_Click(object sender, EventArgs e)
        {

        }

        private void label127_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label82_Click(object sender, EventArgs e)
        {

        }

        private void groupControl5_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void combo_section_inscription_SelectedIndexChanged(object sender, EventArgs e)
        {
            par1.chargementcombo_section_saisir(combo_section_inscription, txtcombo_section_inscription, combo_section_inscription.Text);
            chargerComboAdresse2("option1", "optioneleve", combo_option_inscription, "codesect", txtcombo_section_inscription.Text);
        }

        private void combo_option_inscription_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool teste = ClIntelligence.GetInstance().teste_Option(combo_option_inscription.Text, txtcombo_section_inscription.Text);
            if (teste == true)
            {
                par1.chargementcombo_option_saisir(combo_option_inscription, txtcombo_option_inscription, combo_option_inscription.Text);
                
            }
            else {
                MessageBox.Show("L'Option ne correspond pas a la section !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            
        }

        private void comboannee2_SelectedIndexChanged(object sender, EventArgs e)
        {
            el1.chargementcombo_annee_saisir(comboannee2, txtcombo_annee_inscription, comboannee2.Text);
        }

        private void btnprecedent9_Click_1(object sender, EventArgs e)
        {
            dep--;
            deplacerEnseignant(dep);
        }

        private void simpleButton53_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Voulez-vous Ajouter un eleve ?", "Innitialisation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                try
                {
                    txtMatriculeEns.Text = "";
                    txtNomEns.Text = ""; txtpostnomEns.Text = ""; txtprenomEns.Text = "";
                    txtsexeEns.Text = ""; txtmailEns.Text = "";
                    txtphoneEns.Text = ""; txtdomaineEns.Text = "";
                    txtqualifEns.Text = ""; txtetatcivilEns.Text = "";


                    photoEns.Image = Image.FromFile(Environment.CurrentDirectory + @"\library\elementary_school.png");
                    //photo.Image = null;

                }
                catch
                {
                    MessageBox.Show("Erreur d'innitialiser", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }


        }

        private void simpleButton54_Click_1(object sender, EventArgs e)
        {
            try
            {

                if (txtNomEns.Text == "" | txtpostnomEns.Text == "" | txtprenomEns.Text == "" | txtsexeEns.Text == "" | txtmailEns.Text == "" | txtphoneEns.Text == "" | txtdomaineEns.Text == "" | txtqualifEns.Text == "" | txtetatcivilEns.Text == "")
                {
                    XtraMessageBox.Show("Completez tous les champs svp !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                {

                    string mat= txtpostnomEns.Text.Substring(0, 2) + txtphoneEns.Text.Substring(4,3)+txtNomEns.Text.Substring(1,2)+txtdomaineEns.Text.Substring(0,2);

                    ens.mergeenseignant(mat, txtNomEns.Text, txtpostnomEns.Text, txtprenomEns.Text, txtsexeEns.Text, txtmailEns.Text, txtphoneEns.Text, txtdomaineEns.Text, txtqualifEns.Text, txtetatcivilEns.Text, photoEns.Image);
                    ens.chargementEns(gridEnseignant);
                    cr1.chargementcombo_enseignant_designe(enseignant_affect);
                    cr1.chargementcombo_enseignant_designe(combo_ens_affect_horaire);                   
                }

            }

            catch (Exception ex)
            {
                XtraMessageBox.Show("Erreur d'enregistrement !!!!" + ex.Message);
            }

        }

        private void simpleButton55_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtMatriculeEns.Text == "")
                {
                    XtraMessageBox.Show("Entrez le code de l'Enseignant a supprimer !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                {
                    ens.supprimerEns(txtMatriculeEns.Text);
                    ens.chargementEns(gridEnseignant);
                    cr1.chargementcombo_enseignant_designe(enseignant_affect);
                    cr1.chargementcombo_enseignant_designe(combo_ens_affect_horaire);
                    
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Erreur de suppression !!!!!" + ex.Message);

            }

        }

        private void simpleButton79_Click_1(object sender, EventArgs e)
        {
            try
            {
                carte_service rpt = new carte_service();
                rpt.DataSource = clreport.GetInstance().Identite_enseignant("Enseignant", txtMatriculeEns.Text);
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }
        }

        private void rechercheEns_TextChanged(object sender, EventArgs e)
        {
            ens.rechercheEns(rechercheEns.Text, gridEnseignant);
            deplacerEnseignant(dep);
        }

        private void btnsuivant9_Click_1(object sender, EventArgs e)
        {
            dep++;
            deplacerEnseignant(dep);
        }

        private void gridEnseignant_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dep = e.RowIndex;         
            deplacerEnseignant(dep);
        }

        private void simpleButton20_Click_2(object sender, EventArgs e)
        {
            code_compte.Text = "";
            nom_compte.Text = "";
        }

        private void simpleButton21_Click_2(object sender, EventArgs e)
        {
            try
            {
                ens.insertion_comptable(code_compte.Text, nom_compte.Text);
                gridControl4.DataSource = ens.chargement_comptable();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton22_Click_3(object sender, EventArgs e)
        {
            try
            {
                ens.supprimer_comptable(code_compte.Text);
                gridControl4.DataSource = ens.chargement_comptable();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridControl4_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                code_compte.Text = gridView4.GetFocusedRowCellValue("codeutil").ToString();
                nom_compte.Text = gridView4.GetFocusedRowCellValue("utilisateur").ToString();                
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void combo_section_affect_SelectedIndexChanged(object sender, EventArgs e)
        {
            par1.chargementcombo_section_saisir(combo_section_affect, txtcombo_section_affect, combo_section_affect.Text);
            chargerComboAdresse2("option1", "optioneleve", combocode_option_affect, "codesect", txtcombo_section_affect.Text);
        }

        private void combocode_option_affect_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool teste = ClIntelligence.GetInstance().teste_Option(combocode_option_affect.Text, txtcombo_section_affect.Text);
            if (teste == true)
            {
                par1.chargementcombo_option_saisir(combocode_option_affect, txtcombo_option_affect, combocode_option_affect.Text);
            }
            else {
                MessageBox.Show("L'Option ne correspond pas a la section svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            
        }

        private void enseignant_affect_SelectedIndexChanged(object sender, EventArgs e)
        {
            cr1.chargementcombo_enseignant_saisir(enseignant_affect, txtcombo_ens_affect, enseignant_affect.Text);
        }

        private void annee_affect_SelectedIndexChanged(object sender, EventArgs e)
        {
            el1.chargementcombo_annee_saisir(annee_affect, txtcombo_annee_affect, annee_affect.Text);
        }

        private void combocode_periode_affect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bool teste = ClIntelligence.GetInstance().teste_Periode(txtcombo_annee_affect.Text, classe_affect.Text, combocode_periode_affect.Text);
            //if (teste == true)
            //{
            //    cr1.chargementcombo_periode_saisir(combocode_periode_affect, txtcombo_periode_affect, combocode_periode_affect.Text);
            //}
            //else {
            //    MessageBox.Show("La période precedente n'est pas encore clôturée pour la classe choisie svp !!! veillez contacter l'administrateur !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            //}
            cr1.chargementcombo_periode_saisir(combocode_periode_affect, txtcombo_periode_affect, combocode_periode_affect.Text);
        }

        private void comboperiode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bool teste = ClIntelligence.GetInstance().teste_Periode(Tempannee, TempOption, comboperiode.Text);
            //if (teste == true)
            //{
                cr1.chargementcombo_periode_saisir(comboperiode, txtcombo_periode_cote, comboperiode.Text);
            //}
            //else {
            //    MessageBox.Show("La période precedente n'est pas encore clôturée pour la classe de l'eleve choisi svp !!! veillez contacter l'administrateur !!!!");
            //}
            
        }

        private void comboeleve_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void combo_code_section_affect_horaire_SelectedIndexChanged(object sender, EventArgs e)
        {
           //par1.chargementcombo_section_saisir(combo_code_section_affect_horaire, txtcombo_section_horaire, combo_code_section_affect_horaire.Text);

            par1.chargementcombo_section_saisir(combo_code_section_affect_horaire, txtcombo_section_horaire, combo_code_section_affect_horaire.Text);
            combo_option_affect_horaire.Items.Clear();
            chargerComboAdresse2("option1", "optioneleve", combo_option_affect_horaire, "codesect", txtcombo_section_horaire.Text);
        }

        private void combo_option_affect_horaire_SelectedIndexChanged(object sender, EventArgs e)
        {

            par1.chargementcombo_option_saisir(combo_option_affect_horaire, txtcombo_option_horaire, combo_option_affect_horaire.Text);
            //bool teste = ClIntelligence.GetInstance().teste_Option(combo_option_affect_horaire.Text, txtcombo_section_horaire.Text);
            //if (teste == true)
            //{
            //    par1.chargementcombo_option_saisir(combo_option_affect_horaire, txtcombo_option_horaire, combo_option_affect_horaire.Text);
            //}
            //else
            //{
            //    MessageBox.Show("L'Option ne correspond pas a la section svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            //}            
        }

        private void combo_ens_affect_horaire_SelectedIndexChanged(object sender, EventArgs e)
        {
            cr1.chargementcombo_enseignant_saisir(combo_ens_affect_horaire, txtcombo_ens_horaire, combo_ens_affect_horaire.Text);
        }

        private void combo_anne_affect_horaire_SelectedIndexChanged(object sender, EventArgs e)
        {
            el1.chargementcombo_annee_saisir(combo_anne_affect_horaire, txtcombo_annee_horaire, combo_anne_affect_horaire.Text);

            combo_cours_affect_horaire.Items.Clear();
            ClIntelligence.GetInstance().chargementcombocours(combo_cours_affect_horaire, txtcombo_annee_horaire.Text, combo_classe_affect_horaire.Text, txtcombo_option_horaire.Text, txtcombo_section_horaire.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comb_jours_affect_horaire_SelectedIndexChanged(object sender, EventArgs e)
        {
            hor1.chargementcombo_jours_saisir(comb_jours_affect_horaire, txtcombo_jours_horaire, comb_jours_affect_horaire.Text);
        }

        private void groupControl51_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton8_Click_2(object sender, EventArgs e)
        {
            try
            {
                Palmaresse rpt = new Palmaresse();
                rpt.DataSource = clreport.GetInstance().liste_pourcent_final("Pourcentage_General", combo_classe_pourcent.Text, combo_section_pourcent.Text, combo_option_pourcent.Text);
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }
        }

        private void combo_classe_pourcent_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string choix = combo_classe_pourcent.Text;
            if (choix == "1ere" | choix == "2eme")
            {
                combo_section_pourcent.Enabled = false;
                combo_option_pourcent.Enabled = false;
            }

            else if (choix == "3eme" | choix == "4eme")
            {
                combo_section_pourcent.Enabled = true;
                combo_option_pourcent.Enabled = false;
            }
            else if (choix == "5eme" | choix == "6eme")
            {
                combo_section_pourcent.Enabled = true;
                combo_option_pourcent.Enabled = true;
            }

            else
            {

                XtraMessageBox.Show("Erreur de choix !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void barButtonItem37_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = pagebackupRestauration;
        }

        private void barButtonItem38_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navframe.SelectedPage = pagebackupRestauration;
        }

        private void simpleButton14_Click_2(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {                   
                    case "btnbackup":
                        panelSetting.Controls.Clear();
                        userBuckup ts = new userBuckup();
                        ts.Dock = panelSetting.Dock;
                        panelSetting.Controls.Add(ts);
                        break;
                    case "btnrestauration":
                        panelSetting.Controls.Clear();
                        userrestauration ucRestore = new userrestauration();
                        ucRestore.Dock = panelSetting.Dock;
                        panelSetting.Controls.Add(ucRestore);
                        break;                   
                    //case "btnHomeSetting":
                    //    pnlSetting.Controls.Clear();
                    //    ucSettingHome ucSettingHome = new ucSettingHome();
                    //    ucSettingHome.Dock = pnlSetting.Dock;
                    //    pnlSetting.Controls.Add(ucSettingHome);
                    //    break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERREUR...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void simpleButton24_Click_1(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "btnbackup":
                        panelSetting.Controls.Clear();
                        userBuckup ts = new userBuckup();
                        ts.Dock = panelSetting.Dock;
                        panelSetting.Controls.Add(ts);
                        break;
                    case "btnrestauration":
                        panelSetting.Controls.Clear();
                        userrestauration ucRestore = new userrestauration();
                        ucRestore.Dock = panelSetting.Dock;
                        panelSetting.Controls.Add(ucRestore);
                        break;
                        //case "btnHomeSetting":
                        //    pnlSetting.Controls.Clear();
                        //    ucSettingHome ucSettingHome = new ucSettingHome();
                        //    ucSettingHome.Dock = pnlSetting.Dock;
                        //    pnlSetting.Controls.Add(ucSettingHome);
                        //    break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERREUR...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void simpleButton14_Click_3(object sender, EventArgs e)
        {
            try
            {
                Liste_Inscription_general rpt = new Liste_Inscription_general();
                rpt.DataSource = clreport.GetInstance().liste_eleve_generale("liste_inscription");
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }
        }

        private void simpleButton24_Click_2(object sender, EventArgs e)
        {
            try {
                el1.supprimer_paiement(txtcodePaiement.Text);
                el1.chargementpaiement(gridpaiement,txtcomboAnneeDebut.Text);
                gridControl3.DataSource = ens.chargement_solde_caisse();

                grid_historique.DataSource = el1.chargement_historique_paiement();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void combo_code_livre_SelectedIndexChanged(object sender, EventArgs e)
        {
            bib1.chargementcombocodelivre_saisir(combo_code_livre, txt_combo_livre, combo_code_livre.Text);
        }

        private void pass_MouseEnter(object sender, EventArgs e)
        {
            //pass.BackColor = Color.Yellow;
        }

        private void pass_MouseMove(object sender, MouseEventArgs e)
        {
            //pass.BackColor = Color.White;
        }

        private void pass_MouseLeave(object sender, EventArgs e)
        {
            pass.BackColor = Color.White;
        }

        private void pass_MouseDown(object sender, MouseEventArgs e)
        {
            pass.BackColor = Color.Yellow;
        }

        private void groupControl27_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void combo_code_section_prevision_SelectedIndexChanged(object sender, EventArgs e)
        {
            par1.chargementcombo_section_saisir(cmbSectionPrev, txtComboSectionPrev, cmbSectionPrev.Text);

            //par1.chargementcombo_section_saisir(combo_section_inscription, txtcombo_section_inscription, combo_section_inscription.Text);
            chargerComboAdresse2("option1", "optioneleve", cmbOptionPrev, "codesect", txtComboSectionPrev.Text);
        }

        private void text_param_prev4_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool teste = ClIntelligence.GetInstance().teste_Option(cmbOptionPrev.Text, txtComboSectionPrev.Text);
            if (teste == true)
            {
                par1.chargementcombo_option_saisir(cmbOptionPrev, txtcomboOptionPrev, cmbOptionPrev.Text);
            }
            else {
                MessageBox.Show("L'Option ne correspond pas a la section", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
           
        }

        private void simpleButton27_Click_1(object sender, EventArgs e)
        {
            
            try
            {
                bool teste = ClIntelligence.GetInstance().teste_Option(cmbOptionBulletin.Text, txtComboSectionBulletin.Text);
                if (teste == true)
                {
                    BulletinPage rpt = new BulletinPage();
                    rpt.DataSource = clreport.GetInstance().billetinClasse("Proclamation_Final", txtcomboClasseBulletin.Text, txtComboSectionBulletin.Text, txtcomboOptionBulletin.Text,txtcomboAnneeBulletin.Text);
                    using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                else {
                    MessageBox.Show("L'option est invalide a la section svp !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
               
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }
        }

        private void cmbLivreRetour_SelectedIndexChanged(object sender, EventArgs e)
        {
            bib1.chargementcombocodelivre_saisir(cmbLivreRetour, txtcomboLivreRetour, cmbLivreRetour.Text);
        }

        private void confirme_remise_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void txtnombreLivreEmprunt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void cmbLivreRetour_TextChanged(object sender, EventArgs e)
        {
            //cmbLivreRetour.Items.Clear();
            bib1.chargementcombocodelivre(cmbLivreRetour, cmbLivreRetour.Text);
        }

        private void combo_code_livre_TextChanged(object sender, EventArgs e)
        {
            //combo_code_livre.Items.Clear();
            bib1.chargementcombocodelivre(combo_code_livre, combo_code_livre.Text);
        }

        private void nombre_livre_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

        }

        private void barButtonItem39_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormParametreEncours par = new FormParametreEncours();
            par.ShowDialog();
        }

        private void combocours_TextChanged_1(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_coure_designe_recherche(combocours, combocours.Text);
        }

        private void cmbClasseBulletin_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_classe_text(cmbClasseBulletin, txtcomboClasseBulletin, cmbClasseBulletin.Text);
        }

        private void cmbSectionBulletin_SelectedIndexChanged(object sender, EventArgs e)
        {
            par1.chargementcombo_section_saisir(cmbSectionBulletin, txtComboSectionBulletin, cmbSectionBulletin.Text);
        }

        private void cmbOptionBulletin_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool teste = ClIntelligence.GetInstance().teste_Option(cmbOptionBulletin.Text, txtComboSectionBulletin.Text);

            if (teste == true)
            {
                par1.chargementcombo_option_saisir(cmbOptionBulletin, txtcomboOptionBulletin, cmbOptionBulletin.Text);
            }
            else {
                MessageBox.Show("Option est invalide a la section !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            
        }

        private void cours_affect_TextChanged_1(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_coure_designe_recherche(cours_affect, cours_affect.Text);
        }

        private void name_MouseDown(object sender, MouseEventArgs e)
        {
            name.BackColor = Color.Yellow;
        }

        private void name_MouseLeave(object sender, EventArgs e)
        {
            name.BackColor = Color.White;
        }

        private void cmbanneeBulletin_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_annee_text(cmbanneeBulletin, txtcomboAnneeBulletin, cmbanneeBulletin.Text);
        }

        private void simpleButton28_Click_2(object sender, EventArgs e)
        {
            combocours.Items.Clear();
        }

        private void cmbannee2Bulletin_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_annee_text(cmbannee2Bulletin, txtcomboAnnee2Bulletin, cmbannee2Bulletin.Text);
        }

        private void barButtonItem44_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormRapportComptabilite f = new FormRapportComptabilite();
            f.ShowDialog();
        }

        private void combocommune_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCodeAdresseEleve("commune", "commune", "codecommune", txtcomboCommune, combocommune.Text);
            comboquartier.Items.Clear();
            chargerComboAdresse2("quartier", "quartier", comboquartier, "codecom", txtcomboCommune.Text);
        }

        private void profession_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbanneePresence_SelectedIndexChanged(object sender, EventArgs e)
        {
          //el1.chargementcombo_annee_saisir(cmbanneePresence, txtcomboanneePresence, cmbanneePresence.Text);
        }

        private void barButtonItem44_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormRapportComptabilite f = new FormRapportComptabilite();
            f.ShowDialog();
        }

        private void cmbanneeListeCours_SelectedIndexChanged(object sender, EventArgs e)
        {            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                string database = myconn.Database.ToString();              

                    string cmd = "BACKUP DATABASE [" + database + "] TO DISK='" + @"C:\BackupEcole" + "\\" + database + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";

                using (SqlCommand command = new SqlCommand(cmd, myconn))
                {
                    if (myconn.State != ConnectionState.Open)
                    {
                        myconn.Open();
                    }
                    command.ExecuteNonQuery();
                    myconn.Close();
                    XtraMessageBox.Show("Sauvegarde effectué avec succés", "Confirmation Sauvegarde");
                }

            }
            catch (Exception exc)
            {
                XtraMessageBox.Show(exc.Message);
            }
        }

        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormHoraire f = new FormHoraire();
            f.ShowDialog();
        }

        private void gridControl8_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                if (gridView8.RowCount < 1)
                {
                }
                else
                {
                    cours_affect.Text = gridView8.GetFocusedRowCellValue(gridView8.Columns["cours"]).ToString();
                    
                }

            }
            catch (Exception)
            { }
        }

        private void gridControl13_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                if (gridView13.RowCount < 1)
                {

                }
                else
                {
                    enseignant_affect.Text = gridView13.GetFocusedRowCellValue(gridView13.Columns["NomEns"]).ToString();
                    txtcombo_ens_affect.Text = gridView13.GetFocusedRowCellValue(gridView13.Columns["matriculeEns"]).ToString();

                }

            }
            catch (Exception)
            { }
        }

        private void gridControl14_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                if (gridView14.RowCount < 1)
                {

                }
                else
                {
                    combo_code_livre.Text = gridView14.GetFocusedRowCellValue(gridView14.Columns["titre_livre"]).ToString();
                    txt_combo_livre.Text = gridView14.GetFocusedRowCellValue(gridView14.Columns["code_livre"]).ToString();

                }

            }
            catch (Exception)
            { }
        }

        private void barButtonItem2_ItemClick_2(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormRecouvrement f = new FormRecouvrement();
            f.ShowDialog();
        }

        private void Ecole1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void acceuil_Paint(object sender, PaintEventArgs e)
        {

        }

        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormMenssion fm = new FormMenssion();
            fm.ShowDialog();
        }

        private void barButtonItem5_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormDiscipline fd = new FormDiscipline();
            fd.ShowDialog();
        }

        private void simpleButton6_Click_2(object sender, EventArgs e)
        {
            gridControl12.DataSource = cr1.chargement_proclamation();
        }

        private void barButtonItem8_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormPalmaress fp = new FormPalmaress();
            fp.ShowDialog();
        }

        private void barButtonItem9_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormStatReussiteEchec fs = new FormStatReussiteEchec();
            fs.ShowDialog();
        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            deleteAdress();
        }

        private void radioAvenue_CheckedChanged(object sender, EventArgs e)
        {
            labelid.Text = "Code:";
            labeldesignation2.Text = "Avenu:";
            labelid2.Text = "Quartier:";
            cmbRefAdresse.Enabled = true;
            cmbRefAdresse.Items.Clear();
            par1.chargementcombocodeQuartier(cmbRefAdresse);
            initialiserparame2();      

            GridAdressage.DataSource = par1.chargement_Avenue();
            par1.chargementcombocodeAvenue(comboavenue);
        }

        private void tparam3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioville.Checked == true)
            {
                getCodeAdresse("pays", "pays", "codepays");
            }
            else if (radiocom.Checked == true)
            {
                getCodeAdresse("ville", "ville", "codeville");
            }            
            else if (radioquartier.Checked == true)
            {
                getCodeAdresse("commune", "commune", "codecommune");
            }
            else if (radioAvenue.Checked == true)
            {
                getCodeAdresse("quartier", "quartier", "codequartier");
            }
        }

        private void combonation_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCodeAdresseEleve("pays", "pays", "codepays",txtcomboPays,combonation.Text);
            comboville.Items.Clear();
            chargerComboAdresse2("ville", "ville", comboville, "codepays", txtcomboPays.Text);
        }

        private void comboville_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCodeAdresseEleve("ville", "ville", "codeville", txtcomboVille, comboville.Text);
            combocommune.Items.Clear();
            chargerComboAdresse2("commune", "commune", combocommune, "codeville", txtcomboVille.Text);
        }

        private void comboquartier_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCodeAdresseEleve("quartier", "quartier", "codequartier", txtcomboQuartier, comboquartier.Text);
            comboavenue.Items.Clear();
            chargerComboAdresse2("tAvenue", "DesigneAvenue", comboavenue, "refQuartier", txtcomboQuartier.Text);
        }

        private void comboavenue_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCodeAdresseEleve("tAvenue", "DesigneAvenue", "codeAvenue", txtcomboAvenue, comboavenue.Text);
        }



        private void simpleButton24_Click_3(object sender, EventArgs e)
        {
            updateAdress();
        }

        private void GridAdressage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i;
                i = GridAdressage.CurrentRow.Index;
                txtCodeAdress.Text = GridAdressage[0, i].Value.ToString();
                txtDesigneAdress.Text = GridAdressage[1, i].Value.ToString();
                txtcomboRefAdresse.Text = GridAdressage[2, i].Value.ToString();
            }
            catch (Exception)
            { }
        }

        private void btnconnect1_Click(object sender, EventArgs e)
        {
            if (!EnterNewSettings())
                return;

            Cursor.Current = Cursors.WaitCursor;
            pubCon.comm = new GsmCommMain(port, baudRate, timeout);
            try
            {
                pubCon.comm.Open();
                while (!pubCon.comm.IsConnected())
                {
                    Cursor.Current = Cursors.Default;
                    if (MessageBox.Show(this, "No phone connected.", "Connection setup\n",
                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                    {
                        pubCon.comm.Close();
                        return;
                    }
                    Cursor.Current = Cursors.WaitCursor;
                }
                Output("Successfully connected to the phone.\n");
                XtraMessageBox.Show(this, "Successfully connected to the phone.", "Connection setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pubCon.comm.Close();
                label_statut.BackColor = Color.Yellow;
                label_statut.Text = "Connecté";

                btnconnect.Enabled = false;
                //btndeconnect.Enabled = true;
            }
            catch (Exception ex)
            {
                Output("ERREUR : " + ex.Message);
                Output("");
                MessageBox.Show(this, "Connection error: " + ex.Message, "Connection setup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void cboPort_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cboPort.Text == "19")
            {
                portnumber.Text = "19";
            }
            else
            {
                portnumber.Text = cboPort.Text.Substring(3, 2);
            }
        }

        private void cmbAnneeDebut_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_annee_text(cmbAnneeDebut, txtcomboAnneeDebut, cmbAnneeDebut.Text);
            el1.chargementinscription(dataGridView2, txtcomboAnneeDebut.Text);
            cr1.chargementaffectation(gridaffectation, txtcomboAnneeDebut.Text);
            gridControl5.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
            gridControl_recherche_inscription_emprunt.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
            cr1.chargementcotation(gridcotation, txtcomboAnneeDebut.Text);
            gridControl9.DataSource = bib1.chargement_emprunt(txtcomboAnneeDebut.Text);
            gridControl10.DataSource = bib1.chargement_recherche_emprunt_livre(txtcomboAnneeDebut.Text);
            gridControl11.DataSource = bib1.chargement_remise_livre(txtcomboAnneeDebut.Text);
            el1.chargementpaiement(gridpaiement, txtcomboAnneeDebut.Text);
            hor1.chargement_horaire(gridhoraire, txtcomboAnneeDebut.Text);
            el1.rechercheinscription(gridrecherchepaiement, txtcomboAnneeDebut.Text);
            UserSession.GetInstance().Annee = txtcomboAnneeDebut.Text;
        }

        private void gridpaiement_Click(object sender, EventArgs e)
        {
            try
            {

                txtcodePaiement.Text = gridView15.GetFocusedRowCellValue(gridView15.Columns["Num_Recus"]).ToString();
                cmbElevePaie.Text = gridView15.GetFocusedRowCellValue(gridView15.Columns["CodeInscription"]).ToString();
                txtmontantpay.Text = gridView15.GetFocusedRowCellValue(gridView15.Columns["montantpay"]).ToString();
                txtdatepay.Text = gridView15.GetFocusedRowCellValue(gridView15.Columns["datepay"]).ToString();
                //txtlibelle.Text = gridpaiement[15, dep].Value.ToString();
                //comboutilisateur.Text = gridpaiement[17, dep].Value.ToString();
                cmbtypefrais.Text = gridView15.GetFocusedRowCellValue(gridView15.Columns["Frais"]).ToString();
                txtcomboTypeFrais.Text= gridView15.GetFocusedRowCellValue(gridView15.Columns["Code_frais"]).ToString();
                //affichephotoelve2();

            }
            catch
            {

            }
        }

        private void gridrecherchepaiement_Click(object sender, EventArgs e)
        {
            try
            {               
              cmbElevePaie.Text = gridView16.GetFocusedRowCellValue(gridView16.Columns["codeinscription"]).ToString();
               
            }
            catch
            {

            }
        }

        private void gridControl15_Click(object sender, EventArgs e)
        {
            try
            {

                if (gridView17.RowCount < 1)
                {
                }
                else
                {
                    combo_cours_affect_horaire.Text = gridView17.GetFocusedRowCellValue(gridView17.Columns["cours"]).ToString();

                }

            }
            catch (Exception)
            { }
        }

        private void gridhoraire_Click(object sender, EventArgs e)
        {
            try
            {
                code_affect_horaire.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["codeaffect"]).ToString();
                combo_classe_affect_horaire.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["codeclasse"]).ToString();
                txtcombo_option_horaire.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["codeop"]).ToString();
                combo_option_affect_horaire.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["optioneleve"]).ToString();

                txtcombo_jours_horaire.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["codejours"]).ToString();
                comb_jours_affect_horaire.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["design_jours"]).ToString();

                combo_cours_affect_horaire.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["ccours"]).ToString();
                heure_debut.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["heure_debut"]).ToString();
                heure_fin.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["heure_fin"]).ToString();

                txtcombo_ens_horaire.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["codeens"]).ToString();
                combo_ens_affect_horaire.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["NomEns"]).ToString();

                txtcombo_annee_horaire.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["codeanne"]).ToString();
                combo_anne_affect_horaire.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["annee"]).ToString();

                txtcombo_section_horaire.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["code_section"]).ToString();
                combo_code_section_affect_horaire.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["section"]).ToString();

                combo_division_horaire.Text = gridView18.GetFocusedRowCellValue(gridView18.Columns["affectation"]).ToString();


                // affichephotoEns();

            }
            catch
            {

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource=ClIntelligence.GetInstance().recherche_Eleve(textBox1.Text, UserSession.GetInstance().Annee);
        }

        private void groupControl50_Paint(object sender, PaintEventArgs e)
        {

        }

        private void barButtonItem10_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormEcole f = new FormEcole();
            f.ShowDialog();
        }

        private void cmbEcoleInscrip_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_Ecole_text(txtComboEcoleUnscip, cmbEcoleInscrip.Text);
        }

        private void simpleButton28_Click_3(object sender, EventArgs e)
        {
            bool teste = ClIntelligence.GetInstance().teste_Option(combo_option_inscription.Text, txtcombo_section_inscription.Text);

            if (code_inscription.Text == "" || mateleve2.Text == "" | comboclasse2.Text == "" | txtcombo_annee_inscription.Text == "" | combo_division_inscription.Text == "")
            {
                MessageBox.Show("Remplissez tous le champs svp !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            else
            {

                try
                {

                    if (teste == true)
                    {
                        el1.insertioninscription_final1(int.Parse(code_inscription.Text), mateleve2.Text, comboclasse2.Text, txtcombo_option_inscription.Text, int.Parse(txtcombo_annee_inscription.Text), txtcombo_section_inscription.Text, combo_division_inscription.Text, int.Parse(txtComboEcoleUnscip.Text));
                        el1.chargementinscription(dataGridView2, txtcomboAnneeDebut.Text);
                        el1.chargementcombo_inscription(cmbElevePaie);

                        bib1.chargementcombocodelivre(combo_code_livre);
                        gridControl_recherche_inscription_emprunt.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
                        //gridControl_emprunt.DataSource = bib1.chargement_emprunt();
                        grid_historique.DataSource = el1.chargement_historique_inscription();
                        //el1.chargementcombo_affect_inscrit(combo_report_affect);
                        //el1.chargementcombo_classe_inscrite(combo_report_classe);
                        //el1.chargementcombo_option_inscrit(combo_report_option);
                        //el1.chargementcombo_section_inscrit(combo_report_section);
                        gridControl5.DataSource = el1.chargement_recherche_inscription(txtcomboAnneeDebut.Text);
                    }

                    else
                    {
                        MessageBox.Show("L'option ne correspond pas a la section svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                }

                catch (Exception)
                {
                    XtraMessageBox.Show("Pas de lettre pour l'annee et la classe svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

            }           
        }

        private void simpleButton29_Click_2(object sender, EventArgs e)
        {
            bool teste = ClIntelligence.GetInstance().teste_Option(combocode_option_affect.Text, txtcombo_section_affect.Text);
            bool teste1 = ClIntelligence.GetInstance().teste_Affectation(classe_affect.Text, txtcombo_section_affect.Text, txtcombo_option_affect.Text, txtcombo_annee_affect.Text, cours_affect.Text, txtcombo_periode_affect.Text);
            bool teste3 = ClIntelligence.GetInstance().teste_Periode(txtcombo_annee_affect.Text, classe_affect.Text, combocode_periode_affect.Text);
            if (codeaffect.Text==""||max_affect.Text == "" | cours_affect.Text == "" | txtcombo_annee_affect.Text == "" | txtcombo_ens_affect.Text == "" | classe_affect.Text == "" | txtcombo_periode_affect.Text == "")
            {
                XtraMessageBox.Show("Completez tous les champs svp !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (IsNumeric(max_affect.Text) | IsNumeric(annee_affect.Text))
                    {
                        if (double.Parse(max_affect.Text) > 0)
                        {
                            if (teste == true)
                            {
                                if (teste3 == true)
                                {
                                    cr1.insertionaffectation_final1(int.Parse(codeaffect.Text),double.Parse(max_affect.Text), cours_affect.Text, int.Parse(txtcombo_annee_affect.Text), txtcombo_ens_affect.Text, classe_affect.Text, txtcombo_section_affect.Text, txtcombo_option_affect.Text, txtcombo_periode_affect.Text,int.Parse(txtcomboEcoleAffect.Text));
                                    cr1.chargementaffectation(gridaffectation, txtcomboAnneeDebut.Text);
                                    gridControl12.DataSource = cr1.chargement_proclamation();
                                }
                                else
                                {
                                    MessageBox.Show("La période precedente n'est pas encore clôturée pour la classe choisie svp !!! veillez contacter l'administrateur !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                }

                            }
                            else
                            {
                                MessageBox.Show("L'option ne correspond pas a la section !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Le Maximum doit etre superieur a 0 svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }

                    }

                    else
                    {

                        XtraMessageBox.Show("Pas de lettre svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                }
                catch
                {
                    XtraMessageBox.Show("Pas de lettre !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

            }

        }

        private void cmbEcoleAffect_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_Ecole_text(txtcomboEcoleAffect, cmbEcoleAffect.Text);
        }

        private void cmbEcolePrevision_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_Ecole_text(txtcomboEcolePrevision, cmbEcolePrevision.Text);
        }

        private void simpleButton30_Click_2(object sender, EventArgs e)
        {
            if (radioprevision.Checked == true)
            {
                try
                {
                    bool teste = ClIntelligence.GetInstance().teste_Option(cmbOptionPrev.Text, txtComboSectionPrev.Text);
                    if (txtcodePrev.Text == "" | txtMontantPrev.Text == "" | txtComboAnneePrev.Text == "" | cmbClassePrev.Text == "" | txtComboFraisPrev.Text == "")
                    {
                        MessageBox.Show("Completer tous les champs svp !!!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                    else
                    {
                        try
                        {
                            if (IsNumeric(txtMontantPrev.Text))
                            {
                                if (float.Parse(txtMontantPrev.Text) > 0)
                                {
                                    if (teste == true)
                                    {
                                        par1.insertionprevision_final(int.Parse(txtcodePrev.Text), float.Parse(txtMontantPrev.Text), int.Parse(txtComboAnneePrev.Text), cmbClassePrev.Text, txtComboFraisPrev.Text, txtcomboOptionPrev.Text, txtComboSectionPrev.Text, int.Parse(txtcomboEcolePrevision.Text));
                                        par1.chargementprevision(dataGridView9);
                                        //el1.chargementcombocodeprevision(comboprevision);
                                        initialiserparame3();
                                    }
                                    else
                                    {
                                        MessageBox.Show("L'Option ne correspond pas a la section svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Le montant doit superieur a 0 svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                }

                            }
                            else
                            {
                                MessageBox.Show("Le montant doit etre en en numerique svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }

                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            txtcodePrev.Text = "";
                            txtMontantPrev.Text = "";
                            cmbAnneePrev.Text = "";
                            cmbFraisPrev.Text = "";
                            cmbFraisPrev.Text = "";
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            else if (radioanne.Checked == true)
            {
                if (txtcodePrev.Text == "" || txtMontantPrev.Text == "")
                {
                    MessageBox.Show("Completez tous les champs svp !!!");
                }
                else
                {
                    adresse.Code1 = txtcodePrev.Text;
                    adresse.Designation = txtMontantPrev.Text;
                    ClIntelligence.GetInstance().update_Parametre(adresse, "annee", "codeanne", "annee");
                    par1.chargementanne(dataGridView9);
                }

            }
            else if (radio_frais.Checked == true)
            {
                if (txtcodePrev.Text == "" || txtMontantPrev.Text == "")
                {
                    MessageBox.Show("Completez tous les champs svp !!!");
                }
                else
                {
                    adresse.Code1 = txtcodePrev.Text;
                    adresse.Designation = txtMontantPrev.Text;
                    ClIntelligence.GetInstance().update_Parametre(adresse, "frais", "codefrais", "frais");
                    par1.chargement_frais(dataGridView9);
                }

            }

            else {
                MessageBox.Show("Choisissez une option svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void dataGridView9_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (radioprevision.Checked == true)
            {
                try
                {
                    int i;
                    i = dataGridView9.CurrentRow.Index;
                    txtcodePrev.Text = dataGridView9["codeprev", i].Value.ToString();
                    txtMontantPrev.Text = dataGridView9["montantprev", i].Value.ToString();
                    cmbAnneePrev.Text = dataGridView9["annee", i].Value.ToString();
                    cmbClassePrev.Text = dataGridView9["codecl", i].Value.ToString();
                    cmbFraisPrev.Text = dataGridView9["frais", i].Value.ToString();
                    cmbSectionPrev.Text = dataGridView9["section", i].Value.ToString();
                    cmbOptionPrev.Text = dataGridView9["optioneleve", i].Value.ToString();
                    cmbEcolePrevision.Text = dataGridView9["nomEcol", i].Value.ToString();
                    txtComboSectionPrev.Text = dataGridView9["codesection", i].Value.ToString();
                    txtcomboOptionPrev.Text = dataGridView9["codeoption", i].Value.ToString();
                    txtcomboEcolePrevision.Text = dataGridView9["RefEcole", i].Value.ToString();
                    txtComboAnneePrev.Text = dataGridView9["codeanne", i].Value.ToString();
                    txtComboFraisPrev.Text = dataGridView9["codefrais", i].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (radioanne.Checked == true)
            {
                int i;
                i = dataGridView9.CurrentRow.Index;
                txtcodePrev.Text = dataGridView9["codeanne", i].Value.ToString();
                txtMontantPrev.Text = dataGridView9["annee", i].Value.ToString();
            }
            else if (radio_frais.Checked == true)
            {
                int i;
                i = dataGridView9.CurrentRow.Index;
                txtcodePrev.Text = dataGridView9["codefrais", i].Value.ToString();
                txtMontantPrev.Text = dataGridView9["frais", i].Value.ToString();
            }
            else {

            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbAnneePrev_SelectedIndexChanged(object sender, EventArgs e)
        {
            el1.chargementcombo_annee_saisir(cmbAnneePrev, txtComboAnneePrev, cmbAnneePrev.Text);
        }

        private void cmbFraisPrev_SelectedIndexChanged(object sender, EventArgs e)
        {
            el1.sairie_code_frais(txtComboFraisPrev, cmbFraisPrev.Text);
        }

        private void cmbEcoleHoraire_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_Ecole_text(txtcomboEcoleHoraire, cmbEcoleHoraire.Text);
        }

        private void barButtonItem10_ItemClick_2(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormRaport fr = new FormRaport();
            fr.ShowDialog();
        }

        private void barButtonItem13_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormPresence Pres = new FormPresence();
            Pres.ShowDialog();
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void combocommune_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            getCodeAdresseEleve("commune", "commune", "codecommune", txtcomboCommune, combocommune.Text);
            comboquartier.Items.Clear();
            chargerComboAdresse2("quartier", "quartier", comboquartier, "codecom", txtcomboCommune.Text);
        }

        private void comboville_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            getCodeAdresseEleve("ville", "ville", "codeville", txtcomboVille, comboville.Text);
            combocommune.Items.Clear();
            chargerComboAdresse2("commune", "commune", combocommune, "codeville", txtcomboVille.Text);
        }

        private void comboquartier_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            getCodeAdresseEleve("quartier", "quartier", "codequartier", txtcomboQuartier, comboquartier.Text);
            comboavenue.Items.Clear();
            chargerComboAdresse2("tAvenue", "DesigneAvenue", comboavenue, "refQuartier", txtcomboQuartier.Text);
        }

        private void comboavenue_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            getCodeAdresseEleve("tAvenue", "DesigneAvenue", "codeAvenue", txtcomboAvenue, comboavenue.Text);

        }

        private void combo_cours_affect_horaire_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupControl13_Paint(object sender, PaintEventArgs e)
        {

        }

        ClsParametre parm = new ClsParametre();
        private void simpleButton7_Click_4(object sender, EventArgs e)
        {
            if (radioclasse.Checked == true)
            {
                parm.Code = param1.Text;
                parm.Designation = param2.Text;
                ClIntelligence.GetInstance().update_Parametre(parm, "classe", "codecl", "classe");
                gridparam1.DataSource = ClIntelligence.GetInstance().chargementGrid("classe");
            }
            else if (radiosection.Checked == true)
            {
                parm.Code = param1.Text;
                parm.Designation = param2.Text;
                ClIntelligence.GetInstance().update_Parametre(parm, "section", "codesect", "section");
                gridparam1.DataSource = ClIntelligence.GetInstance().chargementGrid("section");
            }
            else if (radiooption.Checked == true) {
                parm.Code = param1.Text;
                parm.Designation = param2.Text;
                parm.RefCode = param3.Text;
                ClIntelligence.GetInstance().update_Parametre2(parm, "option1", "codeop", "optioneleve", "codesect");
                gridparam1.DataSource = ClIntelligence.GetInstance().chargementGrid("option1");
            }
        }

        private void gridparam1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (radioclasse.Checked == true | radiosection.Checked==true)
            {
                int i;
                i = gridparam1.CurrentRow.Index;
                param1.Text = gridparam1[0, i].Value.ToString();
                param2.Text = gridparam1[1, i].Value.ToString();
            }
            else if (radiooption.Checked == true) {
                int i;
                i = gridparam1.CurrentRow.Index;
                param1.Text = gridparam1[0, i].Value.ToString();
                param2.Text = gridparam1[1, i].Value.ToString();
                param3.Text = gridparam1[2, i].Value.ToString();
            }
        }

        private void simpleButton14_Click_4(object sender, EventArgs e)
        {
            try
            {

                if (txtMatriculeEns.Text == "" | txtNomEns.Text == "" | txtpostnomEns.Text == "" | txtprenomEns.Text == "" | txtsexeEns.Text == "" | txtmailEns.Text == "" | txtphoneEns.Text == "" | txtdomaineEns.Text == "" | txtqualifEns.Text == "" | txtetatcivilEns.Text == "")
                {
                    XtraMessageBox.Show("Completez tous les champs svp !!!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else
                {
                    ens.mergeenseignant(txtMatriculeEns.Text, txtNomEns.Text, txtpostnomEns.Text, txtprenomEns.Text, txtsexeEns.Text, txtmailEns.Text, txtphoneEns.Text, txtdomaineEns.Text, txtqualifEns.Text, txtetatcivilEns.Text, photoEns.Image);
                    ens.chargementEns(gridEnseignant);
                    cr1.chargementcombo_enseignant_designe(enseignant_affect);
                    cr1.chargementcombo_enseignant_designe(combo_ens_affect_horaire);
                }

            }

            catch (Exception ex)
            {
                XtraMessageBox.Show("Erreur d'enregistrement !!!!" + ex.Message);
            }

        }

        private void gridControl5_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton31_Click_3(object sender, EventArgs e)
        {
            clsIntermediaire.GetInstance().NomTable = "cours";
            FormCoursSearch c = new FormCoursSearch();
            c.ShowDialog();
            combo_cours_affect_horaire.Text = clsIntermediaire.GetInstance().Valchamp2;
            //txtRefFourn.Text = clsIntermediaire.GetInstance().ValChamp1;
        }

        private void simpleButton32_Click_2(object sender, EventArgs e)
        {
            clsIntermediaire.GetInstance().NomTable = "cours";
            FormCoursSearch c = new FormCoursSearch();
            c.ShowDialog();
            cours_affect.Text = clsIntermediaire.GetInstance().Valchamp2;
        }
    }
}