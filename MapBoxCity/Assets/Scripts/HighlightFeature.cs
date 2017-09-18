using UnityEngine;
using System.Collections.Generic;

public class HighlightFeature : MonoBehaviour
{
	private List<Color> _original = new List<Color>();
	private Color _highlight = Color.red;
	private List<Material> _materials = new List<Material>();
    private bool _hasFocus;

    void Start()
	{
		foreach (var item in GetComponent<MeshRenderer>().materials)
		{
			_materials.Add(item);
			_original.Add(item.color);
		}
	}

    private void Update()
    {
        if (_hasFocus)
        {
            OnFocus();
        }
        else
        {
            OnFocusLost();
        }

        _hasFocus = false;
    }

    private void OnFocus()
    {
        foreach (var item in _materials)
        {
            item.color = _highlight;
        }
    }

    private void OnFocusLost()
    {
        for (int i = 0; i < _materials.Count; i++)
        {
            _materials[i].color = _original[i];
        }
    }

    public void SetFocus(bool focus)
    {
        _hasFocus = focus;
    }
}