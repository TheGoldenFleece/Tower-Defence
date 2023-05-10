public class WaveStatistics
{
    public int enemiesNumber { get; private set; }
    public int destroyedEnemy = 0;
    public int SurvivedEnemy { get => enemiesNumber - destroyedEnemy; }
    float averageLeftHP = 0;
    public float AverageLeftHP { set => averageLeftHP += value; get => averageLeftHP / SurvivedEnemy; }
    float averageTimeToKill = 0;
    public float AverageTimeToKill { set => averageTimeToKill += value; get => averageTimeToKill / destroyedEnemy; }

    public WaveStatistics(int enemiesNumber) {
        this.enemiesNumber = enemiesNumber;
    }
}
