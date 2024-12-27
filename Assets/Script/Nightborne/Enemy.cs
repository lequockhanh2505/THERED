public class Enemy
{
    public Enemy(string name, int hp, float speed)
    {
        this.Name = name;
        this.Hp = hp;
        this.Speed = speed;
    }

    private string name;
    private int hp;
    private float speed;

    public string Name { get => name; set => name = value; }
    public int Hp { get => hp; set => hp = value; }
    public float Speed { get => speed; set => speed = value; }
}
