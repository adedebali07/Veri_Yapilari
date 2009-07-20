//07233009 - Adem Dedebali

// Editör    : Vim
// Derleyici : mcs

using System;

public class VY107233009
{
	// Destenin, yerin ve kullanicilarin kagitlari
	static Kagit deste = new Kagit (52);
	static Kagit yer = new Kagit (52);
	static Kagit bilgisayar = new Kagit (4);
	static Kagit kullanici = new Kagit (4);

	// Skor bilgileri
	static int kullaniciskor = 0;
	static int bilgisayarskor = 0;

	// Bir önceki adımda atılan kağıt
	static string bironceki;

	public static void Main (string[] args)
	{
		// Deste oluşturulur ve dağıtılır.
		deste.DesteyiOlustur ();
		deste.DesteyiDagit ();

		// Yere 4 adet kagit verilerek oyun baslar
		for (int i = 0; i<4; i++)
			yer.Push (deste.Pop ());

		// Destede kagit kalmayincaya kadar oyun devam eder
		while (!deste.isEmpty ())
		{
			//Kullanıcı ve bilgisayara 4er kagit verilir
			for (int i=0; i<4; i++)
			{
				bilgisayar.Push (deste.Pop ());
				kullanici.Push  (deste.Pop ());
			}

			// Oyuna ilk bilgisayar başlayacak. Onun için
			// bizim elimizdeki kağıt bitene kadar bir dörtlü
			// dönecek
			while (!kullanici.isEmpty ())
			{
				bironceki = yer.UsttekiEleman ();

				// Bilgisayar atışını yapar.
				BilgisayarAtacaginiHazirlasin ();
				yer.Push (bilgisayar.Pop());

				// Karşılaştırma yapılır ve gosterilir
				Karsilastir ("B");
				Goster ();

				// Kullanıcı atışını yapar
				KullaniciAtacaginiHazirlasin ();
				yer.Push (kullanici.Pop ());

				// Karşılaştırma yapılır ve gosterilir
				Karsilastir ("K");
				Goster ();
			}
		}
	}

	// Atılacak kağıdı belirler ve stackın en üstüne 
	public static void BilgisayarAtacaginiHazirlasin ()
	{
		if (yer.ElemanSayisi () != 0)
		{
			// Eğer yerdekinin aynısı varsa onu atsın.
			// Yoksa herhangi birini atsın
			if (bilgisayar.StacktaVarMi (yer.UsttekiEleman ()))
				bilgisayar.EnUsteAl (yer.UsttekiEleman ());
		}
	}

	public static void Karsilastir (string kim)
	{
		// B ise en son bilgisayar, K ise oyuncu oynamistir
		int sayac = 0;

		if (yer.UsttekiEleman () == bironceki)
		{
			while (!yer.isEmpty ())
			{
				yer.Pop ();
				sayac++;
			}

			if (kim == "B")
				bilgisayarskor += sayac;
			else
				kullaniciskor += sayac;
		}

		if (yer.UsttekiEleman()[0] == 'V')
		{
			while (!yer.isEmpty ())
			{
				yer.Pop ();
				sayac++;
			}

			if (kim == "B")
				bilgisayarskor += sayac;
			else
				kullaniciskor += sayac;
		}
	}

	public static void Goster ()
	{

		//Ortalık temizlenir
		// Hocam Console.Clear () fonksiyonunu kullandığım zaman mono derleme yapamıyor.
		// Visual Studio'da test edebilme şansım olmadı. Eğer derlemede problem olursa
		// aşağıdaki satır silinirse problem ortadan kalkacak
		Console.Clear ();
		// İlkin bilgisayarın kağıtları gösterilir
		Console.Write ("          ");
		for (int i = bilgisayar.ElemanSayisi (); i>=0; i--)
			Console.Write ("-------    ");
		Console.WriteLine ();
		
		Console.Write ("          ");
		for (int i = bilgisayar.ElemanSayisi (); i>=0; i--)
			Console.Write ("|     |    ");
		Console.WriteLine ();

		Console.Write ("          ");
 		for (int i = bilgisayar.ElemanSayisi (); i>=0; i--)
			Console.Write ("|  *  |    ");
		Console.WriteLine ();

		Console.Write ("          ");
		for (int i = bilgisayar.ElemanSayisi (); i>=0; i--)
			Console.Write ("|     |    ");
		Console.WriteLine ();

		Console.Write ("          ");
		for (int i = bilgisayar.ElemanSayisi (); i>=0; i--)
			Console.Write ("-------    ");
		Console.WriteLine ("\n");

		// Daha sonra dagitilan destedeki kagitlarin adeti,
		// ortadaki kagit ve adeti ve skor gosterilir
		Console.WriteLine (" -------                  -------               Bilgisayar   Sen");
		Console.WriteLine (" |     |                  |" + Tur (yer.UsttekiEleman ()) + "|               ----------   ---");
		Console.WriteLine (" |  " + deste.ElemanSayisi () +" |                  |" + Adi(yer.UsttekiEleman ()) + 
					"|                   " + bilgisayarskor + "        " + kullaniciskor);
		Console.WriteLine (" |     |                  |     |                               ");
		Console.WriteLine (" -------                  -------                               ");
		Console.WriteLine ();

		// Sonra bizim kagitlarimiz gosterilir
		Console.Write ("          ");
		for (int i = kullanici.ElemanSayisi (); i>0; i--)
			Console.Write ("-------    ");
		Console.WriteLine ();

		Console.Write ("          ");
		for (int i = kullanici.ElemanSayisi (); i>0; i--)
			Console.Write ("|" + Tur (kullanici.ElemaniGetir (i-1)) + "|    ");
		Console.WriteLine ();

		Console.Write ("          ");
		for (int i = kullanici.ElemanSayisi (); i>0; i--)
			Console.Write ("|" + Adi (kullanici.ElemaniGetir (i-1)) + "|    ");
		Console.WriteLine ();

		Console.Write ("          ");
		for (int i = kullanici.ElemanSayisi (); i>0; i--)
			Console.Write ("|     |    ");
		Console.WriteLine ();

		Console.Write ("          ");
		for (int i = kullanici.ElemanSayisi (); i>0; i--)
			Console.Write ("-------    ");
		Console.WriteLine ("\n");
	}

	public static string Adi (string x)
	{
		if (x[1] == '1')
			return "On   ";
		else if (x[1] == '2')
			return "İki  ";
		else if (x[1] == '3')
			return "Üç   ";
		else if (x[1] == '4')
			return "Dört ";
		else if (x[1] == '5')
			return "Beş  ";
		else if (x[1] == '6')
			return "Altı ";
		else if (x[1] == '7')
			return "Yedi ";
		else if (x[1] == '8')
			return "Sekiz";
		else if (x[1] == '9')
			return "Dokuz";
		else if (x[1] == 'A')
			return "As   ";
		else if (x[1] == 'P')
			return "Papaz";
		else if (x[1] == 'K')
			return "Kız  ";
		return "Vale ";
	}

	public static string Tur (string x)
	{
		if (x[0] == 'M')
			return "Maça ";

		else if (x[0] == 'K')
			return "Karo ";

		else if (x[0] == 'H')
			return "Kupa ";

		return "Sinek";
	}

	public static void KullaniciAtacaginiHazirlasin ()
	{
		Console.WriteLine ("Kaçıncı kağıdı atacaksın (En sagdaki 1, sonra 2 seklinde):");
		bool bayrak = true;
		int x;

		while (bayrak == true)
		{
			try
			{
				x = Int16.Parse (Console.ReadLine ());
				if (x > yer.ElemanSayisi ())
					continue;
				kullanici.EnUsteElemaniAl (x);
				break;
			}
			
			catch
			{
				bayrak = true;
			}
		}	
	}	
	
}

public class Kagit
{
	int top;
	int size;
	
	// Maça:M, Karo:K, Kupa:H, Sinek:S
	// Vale:V, Kız:K, Papaz:P, As:A, 10:1, diğerleri sayısal değeri
	// İlk eleman grubunu, ikinci eleman değerini tutar.
	string[] tur;

	public Kagit (int size)
	{
		this.top = -1;
		this.size = size;
		tur = new string[size];
	}

	public bool isEmpty ()
	{
		return (this.top == -1);
	}

	public bool isFull ()
	{
		return (this.top >= this.size - 1);
	}

	public void Push (string eleman)
	{
		if (isFull ())
			Console.WriteLine ("Error... Stack is Full");

		else
		{
			this.top++;
			this.tur[this.top] = eleman;
		}
	}

	// En üstteki elemanı döndürür. Elaman yoksa "FF" döndürür.
	public string Pop ()
	{
		string deleted;

		if (!isEmpty ())
		{
			deleted = this.tur[this.top];
			this.top--;
			return deleted;
		}

		else
			return "FF";
	}

	// Yeni bir deste oluşturur. Deste marketten yeni alınmış gibi sıralıdır.
	public void DesteyiOlustur ()
	{
		string[] grup  = new string[4]    {"M", "K", "H", "S"};
		string[] deger = new string[13]   {"1", "2", "3", "4", 
				  		   "5", "6", "7", "8",
				  		   "9", "V", "K", "P",
				  		   "A"};

		for (int i = 0; i<4; i++)
			for (int j = 0; j<13; j++)
				Push (grup[i] + deger[j]);
	}

	// Test amaçlı yazılmıştır, başka durumlarda program içinde kullanılabilir.
	public void Display ()
	{
		if (isEmpty ())
			Console.WriteLine ("Stack bos...\n");

		else
			for (int i = top; i>=0; i--)
				Console.WriteLine (tur[i]);
	}

	// Kagit turunden iki nesne olusturur ve desteyi o iki nesneye dagitir
	// Sonra rastgele birlestirir. Bu işlemi 10 kere yapar.
	public void DesteyiDagit ()
	{
		Kagit x = new Kagit (52);
		Kagit y = new Kagit (52);

		Random r = new Random ();
		int rastgele;

		for (int k = 0; k<10; k++)
		{
			// İlk 26 kağit x destesine yollanır
			for (int i=0; i<26; i++)
				x.Push (Pop ());

			// Geri kalan kagitlar y destesine yollanır.
			while (!isEmpty ())
				y.Push (Pop ());

			// Sonra x ve y desteleri boşalana kadar rastgele
			// üretilen bir sayıya göre tekrar desteye aktarılır
			while (!x.isEmpty () || !y.isEmpty ())
			{
				rastgele = r.Next (0,2);
				if (rastgele == 0 && !x.isEmpty ())
					Push (x.Pop ());

				else if (rastgele == 1 && !y.isEmpty ())
					Push (y.Pop ());
			}
		}
	}

	// Stacktan çıkacak ilk elemanı döndürür
	public string UsttekiEleman ()
	{
		if (isEmpty ())
			return "FF";

		return tur[top];
	}

	// Stackin eleman sayısını döndürür
	public int ElemanSayisi ()
	{
		return top + 1;
	}

	// İstenilen elemanı stackin en üstüne alır.
	public void EnUsteAl (string ustealinacak)
	{
		string gecici;

		for (int i = top; i>=0; i--)
			if (tur[top] == ustealinacak)
			{
				gecici = tur[top];
				tur[top] = ustealinacak;
				ustealinacak = gecici;
			}
	}

	public void EnUsteElemaniAl (int k)
	{
		string gecici;
		k--;

		gecici = tur[top];
		tur[top] = tur[k];
		tur[k] = gecici;
	}

	public bool StacktaVarMi (string enustteki)
	{

		for (int i = top; i>=0; i--)
			if (tur[top] == enustteki)
				return true;

		return false;
	}

	public string ElemaniGetir (int x)
	{
		return tur[x];
	}
}