#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "prototypes.h"

int main()
{
    FILE *fichier = NULL;
    ouvrirFichier(fichier);

    return 0;
}

void ouvrirFichier(FILE *fichier) {
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
    int caraActuel;
    do {
        caraActuel = fgetc(fichier);
        printf("%c", caraActuel);
    } while (caraActuel != EOF);

}
