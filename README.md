Comment naviguer au sein de notre projet :

S'assurer qu'on possède le package RP-lightweight ! (très important pour les Shaders)
Créer sa planète :

Dans un premier temps, créer un GameObject vide et appelez le par exemple "MyFirstPlanet"

Ensuite, on va créer les deux paramètres qui régissent notre nouvelle planète. On va aller dans Assets/Parameters/ on va faire un clique droit + Color Parameter, et clique droit + Shape Parameter

On remplit les champs comme il le faut dans l'inspecteur (Créer un material au préalable pour la couleur de la planète, un par défaut fera l'affaire). 

Ensuite, on va associer ses deux paramètres aux champs adéquat sur notre Planète (cliquez sur le GameObject récemment créé, et dans l'inspecteur, associez Color Parameters et Shape Parameters avec ceux que l'on vient de créer)

On va cliquer sur le bouton Create, et voilà notre nouvelle planète !

Maintenant on va chercher à lui donner du reliefs et des couleurs. Prenez les valeurs de la planète de test dans la scène fourni pour faire une planète semblable à notre chère Terre.



Comment nous générons les planètes :

Etape 1 : 

Construction de la sphère


Dans un premier temps, nous allons générer la sphère. Nous n'utilisons pas un GameObject pour faire la sphère mais nous allons générer la sphère avec des triangles afin d'avoir la possibilité de modifier la sphère dans les détails et à notre convenance.

Donc pour cela on va partir d'un cube, on va le "gonfler" (inflate) pour passer d'un cube à une sphère.

Pour créer ce cube, nous allons créer chacun des 6 côtés du cube, afin d'avoir le contrôle sur le niveau de détail de la sphère.

Dans un premier script (TerrainFace.cs), 
Pour créer une face, nous allons prendre en compte 3 paramètres : Le mesh à laquelle la face sera associé dans UNITY, sa résolution (le niveau de détail que l'on souhaite) et enfin la normale de la face.

Dans un second script (Planet.cs),
nous allons pouvoir créer notre planète : pour cela on initilise une liste de 6 faces, on initialise les mesh qu'on associe aux "faces" de la planète, et enfin on construit toutes les faces graphiquement.


Etape 2 : 

Modification des paramètres graphiques de la planète. Les éléments sur lesquels nous allons travailler son la couleur, la forme de la planète (elle sera globalement ronde mais possèdera des reliefs que l'on peut aussi appelé bruit).

Pour chacun des paramètres (couleur et forme) de la planète, nous créeons nos 2 propres assets (ColourSettings et ShapeSettings), qui une fois initialisé et associé à une planète nous permettra de modifier les paramètres de cette dernière.

Attention /!\ à chaque planète, nous devons recréer un ColourSettings et un ShapeSettings sinon les planètes qui partagent les mêmes assets auront les mêmes caractéristiques.

Pour créer le paramètre "Couleur", nous associons un Gradient et un Material/

Pour créer le paramètre "Shape", nous associons un Radius (taille de la planète) et une liste de Noise (bruit). Le choix de créer une liste de Noise est justifié par le fait que nous voulons modéliser les reliefs d'une planète. Afin d'avoir plusieurs reliefs de forme différentes nous fusionnons plusieurs types de relief. Concrètement, nous voulons fusionner une plaine qui peut avoir quelque courbes assez plates, avec des montagnes qui a des courbes et des pics beaucoup plus prononcés.

Nous voulons pouvoir éditer les planètes directement en cliquant sur l'Inspector du GameObject (planète) sélectionné (PlanetEditor).

Nous ajoutons ensuite la gestion de bruits sur les planètes.