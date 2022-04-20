# Exercice-3tiers (Solution H2022) Exercice perceptron 3 tiers complété

À la base, cet exercice vise à séparer l'application en 3 couches

- La solution est composée de deux librairies (DAL et BLL) et une application (GUI)
- Les dépendances de ces 3 composantes sont à sens unique GUI --> BLL --> DAL

Cette version est enrichie de :

- Tests unitaires pour les DAL et BLL
- Modèle rigide pour les échantillons de données avec noms des champs et nombre d'attributs et valeurs stricts
- Patron Factory pour arrimer un fichier de données au modèle d'échantillon de données
- Patron Factory pour utiliser un lecteur de fichier DAT ou CSV en fonction de l'extension de fichier
- Patron Prototype/memento minimalistes dans une expérience (peu concluante) visant à éviter certains échantillons pendant l'apprentissage (antirégression)
- Affichage de l'historique des erreurs pendant les cycles d'apprentissages