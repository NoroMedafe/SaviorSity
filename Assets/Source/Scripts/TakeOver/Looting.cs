using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class Looting : MonoBehaviour
{
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private GameObject _point;
   
    private bool _isLooting = false;
    public int _itemInCollection;

    private List<Item> _lootingItems = new();

    private void Update()
    {

        if (Input.GetKey(KeyCode.Mouse1))
        {
            _isLooting = true;
            _collider.enabled = true;
            Debug.Log(_lootingItems.Count);

            if (_lootingItems.Count > 0)
            {
                LootItems();
            }
        }
        else
        {
            _collider.enabled = false;

        }
    }

    private void LootItems()
    {

        foreach (var item in _lootingItems)
        {
            item.ItemSelection(_point);

            _itemInCollection++;

        }

        _lootingItems.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (_isLooting)
        {

            if (other.TryGetComponent(out Item item))
            {
                _lootingItems.Add(item);
            }
        }
    }
}
