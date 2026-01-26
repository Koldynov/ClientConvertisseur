public class Devise
{
    private int id;
    private string? nomDevise;
    private double taux;

    public Devise(int id, string? nomDevise, double taux)
    {
        this.Id = id;
        this.NomDevise = nomDevise;
        this.Taux = taux;
    }

    public int Id
    {
        get
        {
            return this.id;
        }

        set
        {
            this.id = value;
        }
    }

    public string? NomDevise
    {
        get
        {
            return this.nomDevise;
        }

        set
        {
            this.nomDevise = value;
        }
    }

    public double Taux
    {
        get
        {
            return this.taux;
        }

        set
        {
            this.taux = value;
        }
    }
}