using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomer : MonoBehaviour
{
	public class BoomInfo
	{
		public float power;
		public float radius;
		public AnimationCurve curveOfDamage;

		public BoomInfo(float _power, float _radius, AnimationCurve _curveOfDamage)
		{
			power = _power;
			radius = _radius;
			curveOfDamage = _curveOfDamage;
		}
	}

	public static BoomInfo GetNormalExplosion()
	{
		Keyframe[] keyFrames = { new Keyframe(0, 1, 2.44f, -2.44f), new Keyframe(1, 0, -0.09635375f, 0.09635375f) };
		AnimationCurve animationCurve = new AnimationCurve(keyFrames);

		return new BoomInfo(1300, 4, animationCurve);
	}

	public static void AllahAcbar(ParticleSystem boom, Vector3 where, BoomInfo boomInfo, MonoBehaviour causer)
	{
		Instantiate(boom, where, new Quaternion());
		GameObject.Find("Camera").GetComponent<CanShake>().Shake();

		var colliders = Physics.OverlapSphere(where, boomInfo.radius);
		foreach (var c in colliders)
		{
			if (c.gameObject != causer.gameObject)
				if (c.attachedRigidbody != null)
				{
					Vector3 substraction = c.transform.position - where;
					Vector3 normalized = substraction.normalized;
					float magnitude = substraction.magnitude;

					c.attachedRigidbody.AddExplosionForce(boomInfo.power, where, boomInfo.radius);

					Player player = c.GetComponent<Player>();
					if (player != null)
					{
						int damage = (int)(boomInfo.curveOfDamage.Evaluate(magnitude / boomInfo.radius) * 100 * 6);
						if (damage > 0)
							player.GetDamege(damage, causer);
					}
				}
		}
	}
}
