public class CollidableItem : Item
{
    public override void OnCollision(Agent agent)
    {
        TryInitiateInteraction(agent);
    }
}