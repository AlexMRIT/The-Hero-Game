using UnityEngine;
using UnityEngine.UI;

#pragma warning disable

public sealed class BuilderGround : MonoBehaviour, IModule<BuilderGround>
{
    [SerializeField] private Canvas _modelCanvas;
    [SerializeField] private Image _block;
    [SerializeField] private Vector2 _locationSize;
    [SerializeField] private Vector2 _offset;

    private Cell[] _location;

    public BuilderGround Init(params object[] args)
    {
        _location = new Cell[(int)(_locationSize.x * _locationSize.y)];

        for (int x = 0; x < _locationSize.x; x++)
        {
            for (int y = 0; y < _locationSize.y; y++)
            {
                Cell cell = new Cell();

                float posX = (x - (_locationSize.x - 1) / 2.0f) * _offset.x;
                float posY = (y - (_locationSize.y - 1) / 2.0f) * _offset.y;

                cell.X = (int)posX;
                cell.Y = (int)posY;

                cell.CellObject = Instantiate(_block, _modelCanvas.transform);
                cell.CellObject.rectTransform.anchoredPosition = new Vector2(posX, posY);
            }
        }

        return this;
    }

    public Location CreateLocation()
    {
        return new Location().SetLocation(_location, _locationSize, _offset);
    }

    public BuilderGround Get()
    {
        return this;
    }
}