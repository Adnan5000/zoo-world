using System;
using System.Threading.Tasks;
using Arch.Views.Mediation;
using UnityEngine;
using ZooWorld.Scripts.Common;
using ZooWorld.Scripts.Models.Animal;
using Random = UnityEngine.Random;

namespace ZooWorld.Scripts.Views.Animals
{
    public class GameplayAnimalView: View, IGameplayAnimalView
    {
        private AnimalServiceModel _animalServiceModel = new AnimalServiceModel();
        public Action<GameplayAnimalView, GameplayAnimalView> OnCollisionEnterAction { get; set; }
        public Action<Vector3> OnSpawnTastyText { get; set; }
        public Action OnDie { get; set; }

        public GameObject GetGameObject => this.gameObject;
        public string GivenName;

        private void Start()
        {
            Movement();
        }

        public void SetModel(AnimalServiceModel model)
        {
            _animalServiceModel = model;
        }

        public void SetName(string name)
        {
            GivenName = name;
        }
        
        public AnimalServiceModel GetModel()
        {
            return _animalServiceModel;
        }
        
        public GameplayAnimalView GetView()
        {
            return this;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            var otherAnimal = collision.gameObject.GetComponent<GameplayAnimalView>();
            var thisAnimal = GetComponent<GameplayAnimalView>();

            if (otherAnimal != null && thisAnimal != null)
            {
                OnCollisionEnterAction?.Invoke(thisAnimal, otherAnimal);
            }
        }
        
        public void Eat()
        {
            OnSpawnTastyText?.Invoke(GetView().transform.position);
        }

        public void Die()
        {
            OnDie?.Invoke();
        }
        
        public void Movement()
        {
            switch (GetModel().AnimalType)
            {
                case EnumsHandler.AnimalType.Predator:
                    LinearMovement();
                    break;
                case EnumsHandler.AnimalType.Prey:
                    JumpMovement();
                    break;
            }
        }
        
        public async Task LinearMovement()
        {
            Vector3 _movementDirection = Vector3.zero;
            float _moveSpeed = 3f;                 
            float _directionChangeInterval = 3f;   
            Rigidbody _rigidbody = GetComponent<Rigidbody>();
    
            float timeSinceLastDirectionChange = 0f; 

            ChangeDirection();

            while (true)
            {
                _rigidbody.MovePosition(_rigidbody.position + _movementDirection * _moveSpeed * Time.deltaTime);

                timeSinceLastDirectionChange += Time.deltaTime;

                if (timeSinceLastDirectionChange >= _directionChangeInterval)
                {
                    ChangeDirection();
                    timeSinceLastDirectionChange = 0f;
                }

                await Task.Yield();
            }
    
            void ChangeDirection()
            {
                _movementDirection = GetRandomDirection();
            }
            
            Vector3 GetRandomDirection()
            {
                return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            }
        }
        
        public async Task JumpMovement()
        {
            float _jumpInterval = 2f;
            float _jumpForce = 5f; 
            Rigidbody _rigidbody = GetComponent<Rigidbody>();
            
            while (true)
            {
                Jump();
                await Task.Delay((int)(_jumpInterval * 1000));
            }
            
            void Jump()
            {
                Vector3 jumpDirection = new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f)).normalized;
                _rigidbody.AddForce(jumpDirection * _jumpForce, ForceMode.Impulse);
            }
        }
    }
}