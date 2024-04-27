using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IAnimalRepository
    {
        IEnumerable<Animal> GetAnimals();

        Animal GetAnimal(int id);
        public int CreateAnimal(Animal animal);
        public int DeleteAnimal(int id);
        public int UpdateAnimal(Animal animal);
    }
}