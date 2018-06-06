using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILayerGenerator {

	HeightmapLayer generateLayer(int weight);

}
