public class Edge
{
    private int From;
    private int To;
    private float Weight;
    private int Id;

    public Edge(int from, int to, float weight, int id)
    {
        this.From = from;
        this.To = to;
        this.Weight = weight;
        this.Id = id;
    }

    public int getFrom()
    {
        return this.From;
    }

    public int getTo()
    {
        return this.To;
    }

    public float getWeight()
    {
        return this.Weight;
    }

    public int getId()
    {
        return this.Id;
    }
}
