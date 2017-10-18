
On souhaite exposer une API pour r�server des salles de r�union.
Cette API permet de
- Lister des salles 
- Cr�er des r�servations 
- Supprimer des r�servations 
- En cas de conflit (de r�servation), l�api doit proposer tous les cr�neaux libres de la journ�e demand�e.
 
Pour simplifier au maximum l�exercice
 
1) il y a 10 salles ( � room0 � � � room9 � )
2) une r�servation est
- Au nom d�une seule personne ( param�tre � user � )
- Concerne toujours une seule salle ( param�tre � room �)
- Sur un cr�neau d�but / fin - la journ�e est d�coup�e en cr�neaux d�1 heure (24 cr�neaux donc) 
- Jamais sur plusieurs jours 

3) le back-end n�a pas d�importance, peut-�tre in-memory par exemple. Mais fonctionnellement, vous devez g�rer les conflits (donc maintenir un �tat) � cf ci-dessus
( Le but de ce kata n�est pas de d�montrer une API scalable/distribu�e/load-balanc�e, avec un back-end persist�.)
 
Le but de ce kata est de montrer
- Votre approche de mod�lisation � restful � de cette API (url paths, status codes �)
- Votre maitrise d�asp.net webapi (owin, etc.) � vous pouvez hoster dans IIS ou en self-hosted, peu importe
- Vous incorporerez https://www.nuget.org/packages/Swashbuckle pour exposer le swagger de cette API et interagir avec dynamiquement (� try out �).
- Le swagger doit exposer des valeurs exemple (payload in/out de chaque operation)
- xDD : a votre libre convenance

