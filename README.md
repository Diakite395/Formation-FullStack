# 📘 Cahier des charges – Plateforme de gestion de tâches *TaskFlow Premium*

## 1. Présentation du projet
Le projet **TaskFlow Premium** est une plateforme web de gestion de tâches et de projets collaboratifs.  
Elle permet aux utilisateurs de gérer leurs tâches quotidiennes, de créer des projets partagés et d’accéder à des fonctionnalités avancées via un abonnement premium.  

L’application sera construite avec :  
- **Frontend** : React + TailwindCSS  
- **Backend (multi-technologies)** : Django (Python), Express (Node.js), .NET (C#), Spring Boot (Java)  
- **Base de données** : MySQL, PostgreSQL ou MongoDB (interchangeable)  
- **Paiement** : Stripe ou PayPal pour l’abonnement premium  

---

## 2. Objectifs
- Proposer une plateforme **moderne et responsive** accessible sur web et mobile.  
- Mettre en place une **architecture multi-backend** avec un frontend unique.  
- Permettre une gestion flexible des données (SQL et NoSQL).  
- Offrir un système d’abonnement premium avec paiement sécurisé.  
- Démontrer les compétences fullstack de l’auteur (frontend + backend).  

---

## 3. Fonctionnalités principales

### 3.1 Utilisateurs
- Inscription / Connexion (Email + mot de passe)  
- Authentification via **JWT**  
- Rôles :  
  - **Utilisateur gratuit** : gestion de tâches personnelles uniquement  
  - **Utilisateur premium** : accès aux projets collaboratifs  
  - **Admin** : gestion des utilisateurs et supervision  

### 3.2 Gestion des tâches (CRUD)
- Créer une tâche : titre, description, échéance, priorité, statut (*à faire / en cours / terminé*)  
- Consulter la liste des tâches (filtrer par statut, priorité, date)  
- Modifier une tâche  
- Supprimer une tâche  
- Déplacer une tâche (drag & drop Kanban)  

### 3.3 Gestion des projets
- Créer un projet (premium uniquement)  
- Ajouter des membres au projet (invitation par email)  
- Définir des rôles dans un projet (lecteur / contributeur / admin)  
- Tableau de tâches partagé entre membres  

### 3.4 Tableau de bord
- Nombre total de tâches créées / terminées  
- Projets en cours  
- Notifications (rappels de tâches à échéance)  
- Statistiques visuelles (graphiques simples)  

### 3.5 Paiement & Abonnement Premium
- Intégration **Stripe** ou **PayPal**  
- Gestion des abonnements :  
  - Gratuit → Premium (mensuel ou annuel)  
  - Historique des paiements  
- Gestion automatique des rôles après paiement validé  
- Webhooks Stripe/PayPal pour mise à jour en temps réel  

---

## 4. Contraintes techniques
- **Frontend** : React, TailwindCSS, Axios, React Router  
- **Backends** :  
  - Django REST Framework (Python)  
  - Express.js + Sequelize/Mongoose (Node.js)  
  - ASP.NET Core Web API + Entity Framework (C#)  
  - Spring Boot + JPA/Hibernate (Java)  
- **Base de données** :  
  - SQL : MySQL ou PostgreSQL  
  - NoSQL : MongoDB  
- **API REST** : tous les backends doivent respecter les mêmes routes et formats de réponse  
- **Paiement** : Stripe/PayPal SDK  

---

## 5. Architecture technique
\`\`\`
[ React + Tailwind (Frontend) ]
            |
    -------------------------
    |           |           |
[ Backend 1 ] [ Backend 2 ] [ Backend 3 ... ]
(Django)     (Express)     (.NET / Spring Boot)
            |
    -------------------------
    |           |           |
 [ MySQL ]   [ PostgreSQL ] [ MongoDB ]
            |
    [ API Paiement (Stripe/PayPal) ]
\`\`\`

---

## 6. Modèles de données (simplifiés)

### **Utilisateur**
- id  
- nom, email, mot_de_passe (hash)  
- rôle (gratuit, premium, admin)  
- date_inscription  
- statut_abonnement  

### **Tâche**
- id  
- titre, description  
- statut (à faire / en cours / terminé)  
- priorité (basse, moyenne, haute)  
- date_création, date_echeance  
- id_utilisateur / id_projet  

### **Projet**
- id  
- nom, description  
- id_utilisateur_propriétaire  
- liste_membres (relation utilisateur)  

### **Paiement**
- id  
- id_utilisateur  
- montant, devise  
- type (Stripe / PayPal)  
- statut (en attente, validé, annulé)  
- date  

---

## 7. Sécurité
- Authentification via **JWT**  
- Hachage des mots de passe (bcrypt/argon2)  
- Validation des entrées côté backend  
- HTTPS obligatoire pour le déploiement  
- Webhooks sécurisés pour les paiements  

---

## 8. Déploiement
- **Frontend** : Vercel / Netlify  
- **Backend** : Docker + hébergement cloud (AWS, Azure, GCP, Render, Railway)  
- **Base de données** : services managés (RDS, MongoDB Atlas)  
- **Nom de domaine + certificat SSL**  

---

## 9. Planning prévisionnel
- **Semaine 1-2** : conception base de données + API endpoints  
- **Semaine 3-4** : développement du frontend (auth + CRUD tâches)  
- **Semaine 5-6** : implémentation multi-backend  
- **Semaine 7** : intégration paiement premium  
- **Semaine 8** : tests + déploiement  

---

## 10. Livrables
- Code source (frontend + backends)  
- Documentation API (Swagger ou Postman)  
- Base de données (scripts SQL ou JSON pour MongoDB)  
- Guide d’installation et déploiement  
- Démo en ligne du projet  
