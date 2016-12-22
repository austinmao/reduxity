using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CubeManager : IFixedTickable, ITickable {

	readonly List<Cube> cubes_ = new List<Cube>();
	readonly Settings settings_;
	readonly Cube.Factory cubeFactory_;

	public CubeManager(Settings settings, Cube.Factory cubeFactory) {
		settings_ = settings;
		cubeFactory_ = cubeFactory;
	}

	public IEnumerable<Cube> Cubes {
		get { return cubes_; }
	}

	public void FixedTick() {
		foreach (var cube in cubes_) {
			cube.FixedTick();
		}
	}

    public void Tick() {
		foreach (var cube in cubes_) {
			// currently does nothing
			cube.Tick();
		}

		if (cubes_.Count < settings_.maxSpawns) {
			// Debug.Log($"spawning next. count = {cubes_.Count},  maxSpawns = {settings_.maxSpawns}");
			SpawnNext();
		}
    }

    public void SpawnNext() {
		var cube = cubeFactory_.Create();
		cube.Position = GetRandomStartPosition();

		// add to cubes list
		// Debug.Log($"before: {cubes_.Count}");
		cubes_.Add(cube);
		// Debug.Log($"after: {cubes_.Count}");
    }

    public Vector3 GetRandomStartPosition() {
		var rand = UnityEngine.Random.Range(0.0f, 1.0f);
		return new Vector3(rand, rand, rand);
    }

    [Serializable]
	public class Settings {
		public int maxSpawns;
	}
}
