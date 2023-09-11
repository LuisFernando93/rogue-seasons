public class Edge
{
    private int From;
    private int To;
    private int Weight;
    private int Id;

    public Edge(int from, int to, int weight, int id)
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

    public int getWeight()
    {
        return this.Weight;
    }

    public int getId()
    {
        return this.Id;
    }
}
