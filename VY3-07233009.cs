// VY3a - 07233009 - Adem Dedebali

// Derleyici : mono
// Editör : Kate

using System;

public class Node
{
	public int matematiknotu;
	public int fiziknotu;

	public string name;
	
	public Node nextmat;
	public Node nextfiz;

	public Node (int matematiknotu, int fiziknotu, string name)
	{
		this.matematiknotu = matematiknotu;
		this.fiziknotu = fiziknotu;

		this.name = name;

		this.nextmat = null;
		this.nextfiz = null;
	}
}

public class LinkedList
{
	Node headmat;
	Node headfiz;

	public void AddAsSorted (int matnot, int fiznot, string name)
	{
		Node temp = new Node (matnot, fiznot, name);

		// İlkin listenin boşluğuna bakılır.
		// Biri null ise diğeri de nulldur
		if (headmat == null)
		{
			headmat = temp;
			headfiz = temp;
		}

		// Liste boş değilse matematik ve fizik içim ayrı
		// kontroller yapılır
		else
		{
			// Matematik notuna uygun yer bulunur.
			// Listenin başına mı gelecek?
			if (headmat.matematiknotu >= matnot)
			{
				temp.nextmat = headmat;
				headmat = temp;
			}

			// Yoksa herhangi bir yerine mi?
			else
			{
				Node iterator = headmat;
				Node prev = iterator;

				while (iterator != null && iterator.matematiknotu < matnot)
				{
					prev = iterator;
					iterator = iterator.nextmat;
				}

				temp.nextmat = iterator;
				prev.nextmat = temp;
			}

			// Fizik kontrolleri yapılır
			if (headfiz.fiziknotu >= fiznot)
			{
				temp.nextfiz = headfiz;
				headfiz = temp;
			}

			// Yoksa herhangi bir yerine mi?
			else
			{
				Node iterator = headfiz;
				Node prev = iterator;

				while (iterator != null && iterator.fiziknotu < fiznot)
				{
					prev = iterator;
					iterator = iterator.nextfiz;
				}

				temp.nextfiz = iterator;
				prev.nextfiz = temp;
			}
		}
	}

	// 1 gönderilirse matematiğe göre, 0 gönderilirse fiziğe göre listeler
	private void Display (bool x)
	{
		Node iterator;

		if (x)
			iterator = headmat;

		else
			iterator = headfiz;

		Console.WriteLine ("Matematik  /  Fizik  /  Adi\n");
		while (iterator != null)
		{
			Console.WriteLine ("    " +iterator.matematiknotu + "            " + iterator.fiziknotu + "    " + iterator.name);

			if (x)
				iterator = iterator.nextmat;
			else
				iterator = iterator.nextfiz;
		}

		Console.WriteLine ();
	}

	public void DisplayMat ()
	{
		Display (true);
	}

	public void DisplayFiz ()
	{
		Display (false);
	}

	public void DeleteByName (string name)
	{
		// Boşluk kontrol edilir
		if (headmat == null)
		{
			Console.WriteLine ("Liste bos...");
			return;
		}

		// Matematik listesinin ilk elemani ise head ileri alınır
		if (headmat.name.CompareTo (name) == 0)
			headmat = headmat.nextmat;

		else
		{
			Node iterator = headmat;
			Node prev = iterator;

			while (iterator != null && iterator.name.CompareTo (name) != 0)
			{
				prev = iterator;
				iterator = iterator.nextmat;
			}

			if (iterator != null)
				prev.nextmat = iterator.nextmat;
		}

		// Aynı işlem fizik için de yapılır
		if (headfiz.name.CompareTo (name) == 0)
			headfiz = headfiz.nextfiz;

		else
		{
			Node iterator = headfiz;
			Node prev = iterator;

			while (iterator != null && iterator.name.CompareTo (name) != 0)
			{
				prev = iterator;
				iterator = iterator.nextfiz;
			}

			if (iterator != null)
				prev.nextfiz = iterator.nextfiz;
		}
			
	}
}

public class Test
{
	public static void Main (string[] args)
	{
		LinkedList x = new LinkedList ();

		Console.WriteLine ("Boş listeye eleman ekleme testi");
		x.AddAsSorted (10,90, "ali");
		x.DisplayMat ();
		x.DisplayFiz ();

		Console.WriteLine ("Dolu listeye eleman ekleme testi");
		x.AddAsSorted (15, 95, "veli");
		x.DisplayMat ();
		x.DisplayFiz ();

		Console.WriteLine ("Fizik ve matematiğin en başına gelmesi gereken elemanın eklenmesi testi");
		x.AddAsSorted (5, 70, "deli");
		x.DisplayMat ();
		x.DisplayFiz ();

		Console.WriteLine ("İki listenin de arasında olacak elemanın eklenmesi testi");
		x.AddAsSorted (12, 80, "kamber");
		x.DisplayMat ();
		x.DisplayFiz ();

		Console.WriteLine ("Matematik listesinin başındaki elemanın silinmesi testi");
		x.DeleteByName ("deli");
		x.DisplayMat ();
		x.DisplayFiz ();

		Console.WriteLine ("Fizik listeninin başındaki elemanın silinmesi testi");
		x.DeleteByName ("kamber");
		x.DisplayMat ();
		x.DisplayFiz ();

		Console.WriteLine ("Matematik listesinin sonundaki elemanın silinmesi testi");
		x.DeleteByName ("veli");
		x.DisplayMat ();
		x.DisplayFiz ();

		Console.WriteLine ("Listedeki son elemanın silinmesi testi");
		x.DeleteByName ("ali");
		x.DisplayMat ();
		x.DisplayFiz ();

		Console.WriteLine ("Olmayan elemanı silmeye çalışma testi");
		x.DeleteByName ("asd");
		x.DisplayMat ();
		x.DisplayFiz ();

		Console.WriteLine ("Boşaltılan listeye eleman ekleme testi");
		x.AddAsSorted (0,0, "mono");
		x.DisplayMat ();
		x.DisplayFiz ();

		Console.WriteLine ("Bütün testler başarılı");
	}
}