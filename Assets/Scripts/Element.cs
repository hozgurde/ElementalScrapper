

public class ElementCombination
{
    ElementType element1;
    ElementType element2;

    public ElementCombination()
    {
        element1 = ElementType.None;
        element2 = ElementType.None;
    }

    public ElementCombination(ElementType element1, ElementType element2)
    {
        this.element1 = element1;
        this.element2 = element2;
    }

    public ElementType GetFirstElement()
    {
        return element1;
    }

    public ElementType GetSecondElement()
    {
        return element2;
    }

    public void SetFirstElement(ElementType element)
    {
        element1 = element;
    }
    public void SetSecondElement(ElementType element)
    {
        element2 = element;
    }
}
public class Element
{
    private ElementType _elementType;

    public Element(ElementType elementType)
    {
        _elementType = elementType;
    }

    public ElementType GetElementType()
    {
        return _elementType;
    }
}
