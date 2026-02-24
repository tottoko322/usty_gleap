using UnityEngine;

public interface IHasStatusManager
{
    StatusManager Status { get; }
}

//StatusManagerに継承させることで、StatusManagerがあるかどうかの判断ができる。