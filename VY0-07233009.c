// 07233009 - Adem Dedebali

// Stacki bağlı liste kullanarak yaptım. Bağlı liste kendisinden önceki elemanın adresini tutuyor.
// Böylece stack yapısı kuruluyor.

// Editör : Vim
// Derleyici : gcc

#include <stdio.h>

struct node
{
	int value;
	struct node *back;
};

int IsEmpty (struct node *nod);
struct node * Push (struct node *stack, int value);
int IsFull (struct node *nod);
void Display (struct node *nod);
int Pop (struct node * *stack);
struct node *DegerSil (struct node *nod, int value);

int main (int argc, char argv[])
{ 
	struct node *stack = NULL;

	printf ("IsEmpy () ve IsFull () fonksiyonlari zaten diger fonksiyonlar kullanilirken\n");
	printf ("otomatik olarak test ediliyor\n\n");

	printf ("Bos stacke eleman ekleme testi: 5 eklenir.\n");
	stack = Push (stack, 5);
	Display (stack);

	printf ("Dolu stacke eleman ekleme testi: 7 ve 9 eklenir.\n");
	stack = Push (stack, 7);
	stack = Push (stack, 9);
	Display (stack);

	printf ("Dolu Stackten eleman silme testi : Bir eleman silinir\n");
	Pop (&stack);
	Display (stack);

	printf ("Dolu stackten eleman silme testi : Bir eleman silinir\n");
	Pop (&stack);
	Display (stack);

	printf ("Tek elemani olan stackten eleman silme testi : Son eleman silinir\n");
	Pop (&stack);
	Display (stack);

	printf ("Bosaltilan stacke tekrar eleman ekleme testi : 1,3,5,7,9 eklenir\n");
	stack = Push (stack, 1);
	stack = Push (stack, 3);
	stack = Push (stack, 5);
	stack = Push (stack, 7);
	stack = Push (stack, 9);
	Display (stack);

	printf ("Istenilen elemanin silinme testi : 5 silinir\n");
	stack = DegerSil (stack, 5);
	Display (stack);

	printf ("Istenilen eleman stackin son elemaniysa : 1 silinir\n");
	stack = DegerSil (stack, 1);
	Display (stack);

	printf ("Istenilen eleman stackin ilk elemaniysa : 9 silinir\n");
	stack = DegerSil (stack, 9);
	Display (stack);

	printf ("Kalan 3 ve 7 istege bagli silinir ve 25 eklenir\n");
	stack = DegerSil (stack, 3);
	stack = DegerSil (stack, 7);
	stack = Push (stack, 25);
	Display (stack);

	printf ("Butun test islemleri basarili...\n");
	
	return 0;
}

struct node * Push (struct node *stack, int value)
{
	// Yeni eleman için yer istenir
	struct node *new = (struct node *) malloc (sizeof (struct node));

	// Eğer ramda yer yoksa malloc fonksiyonu NULL (0) döndürecektir.
	if (IsFull (new) == 1)
	{
		printf ("Yeni alan için yer yok. %d stacke eklenemedi...\n");
		return stack;
	}

	new->value = value;
	new->back = stack;

	return new; 
}

// Boşsa 1, boş değilse 0 döndürür.
int IsEmpty (struct node *nod)
{
	if (nod == NULL)
		return 1;

	return 0;
}

// Stack'i bağlı liste kullanarak hazırladığım için bu programda bu fonksiyon
// işletim sistemi yeni elemana alan açamadığı zaman işe yarayacak

// Yer açılmazsa 1, aksi durumda 0 döner
int IsFull (struct node *nod)
{
	if (nod == NULL)
		return 1;

	return 0;
}

void Display (struct node *nod)
{
	// Stackın elemanları yazılırken kaçıncı eleman olduğunu söyleyecek değişken
	int counter = 1;

	struct node *iterator = nod;

	if (IsEmpty (nod) == 1)
		printf ("Stack bos... Listeleyemem\n");

	else
		while (iterator != NULL)
		{
			printf ("%4d. eleman : %d\n", counter, iterator->value);
			iterator = iterator->back;
			counter++;
		}

	printf ("\n");
}

// Stackten bir eleman çıkarır ve çıkardığı elemanı döndürür
// Eğer stack boşsa ekrana yazarak uyarır ve -1 döndürür

// Ayrıca stack elemanı ve arkasındakinin adresini tutan bir bağlı liste olduğu için
// stacktan çıkarılan elemandan sonra stackin baş elemanını gösteren pointerın adresi
// bir önceki elemanın adresini gösterir. Bu sebeple fonksiyona işaretçinin işaretçisi
// gönderilir ki hem çıkarılan değer dönsün, hem de stackin ilk elemanının adresi değişebilsin
int Pop (struct node * *stack)
{
	struct node *deleted = *stack;

	// Stackin boş olma durumu kontrol edilir.
	if (IsEmpty (deleted) == 1)
	{
		printf ("Stack bos... Eleman cikaramam\n");
		return -1;
	}

	// Stack silinen elemanın bir önceki elemanına eşitlenir
	int cikarilan = deleted->value;
	*stack =  deleted->back;

	// Çıkarılan eleman serbest bırakılır
	free (deleted);

	return cikarilan;
}

struct node *DegerSil (struct node *nod, int value)
{
	if (IsEmpty (nod) == 1)
	{
		printf ("Stack bos... Deger silemem\n");
		return nod;
	}

	// İlkin stackta ilk elemandan başlayarak geziniriz. Gezinirken çıkardığımız elemanı yeni oluşturduğumuz
	// geçici stacke aktarırız. Aranan bulunursa elemanı serbest bırakıp geçici stackı tekrar ana stacka
	// aktarırız. Eğer bulamazsak yine geçici stackı olduğu gibi ana stacka aktarırız

	struct node *iterator = nod;
	struct node *temp = NULL;

	int silinecek;

	while (iterator != NULL)
	{
		// Stackten elemanı istenir
		silinecek = Pop (&iterator);

		// Eğer aradığımız eleman buysa serbest bırakılır ve döngüden çıkılır
		if (silinecek == value)
			break;

		// Aksi durumda eleman geçici stacke aktarılır.
		temp = Push (temp, silinecek);
	}

	// Döngü bittiğinde silinecek eleman varsa zaten silinmiştir. Geçici stack diğer stacke aktarılır.
	while (temp != NULL)
		iterator = Push (iterator, Pop (&temp));

	return iterator;
}