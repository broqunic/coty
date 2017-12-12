#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "prototypes.h"

int main()
{
    FILE *fichier = NULL;
    char[] fichierEntier = ouvrirFichier(fichier);
	char[][] tabLine = SplitFile(fichierEntier);
	
    return 0;
}

char[] ouvrirFichier(FILE *fichier) {
    fichier = fopen("coty.csv", "r+");
    if (fichier != NULL)

    {
        // On peut lire et écrire dans le fichier
        printf("ouverture du fichier reussie");
        //fputc('B', fichier); // Écriture du caractère A
    }
    else
    {
        // On affiche un message d'erreur si on veut
        printf("Impossible d'ouvrir le fichier coty.csv");
    }
    char[] fichierEntier = "";
    do {
        fichierEntier += fgetc(fichier);
    } while (caraActuel != EOF);

}

char[][] SplitFile(char[] fichierEntier)
{
	int lenght = ft_strlen(fichierEntier);
	char[][] tabSortie;
	int i;
	int x = 0;
	int y = 0;
	for( i=0; i < lenght; i++)
	{
		while (fichierEntier[i] != '\0')
		{
			tabSortie[x][y] = fichierEntier[i];
			y++;
		}
		y = 0;
		x++;
	}
	return tabSortie;
}

int ft_strlen(char *str)
{
	int i;

	i = 0;
	while (str[i] != '\0')
		i++;
	return (i);
}