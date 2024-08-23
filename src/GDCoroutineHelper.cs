using System.Collections.Generic;
using Godot;

public static class GDCoroutineHelper
{
    public static IEnumerator<double> AsCoroutine(this Tween tween) {

        bool end = false;

        tween.TweenCallback(Callable.From(() => end = true));

        while(!end) {
			yield return MEC.Timing.WaitForOneFrame;
		}
    }

    public static IEnumerator<double> AsCoroutine(this AudioStreamPlayer player) {

        bool end = false;

        void f() {        
            end = true;
            player.Finished -= f;
        }

        player.Finished += f;

        while(!end) {
			yield return MEC.Timing.WaitForOneFrame;
		}
    }

    public static IEnumerator<double> AsCoroutine(this AudioStreamPlayer2D player) {

        bool end = false;

        void f() {        
            end = true;
            player.Finished -= f;
        }

        player.Finished += f;

        while(!end) {
			yield return MEC.Timing.WaitForOneFrame;
		}
    }

    public static IEnumerator<double> AsCoroutine(this AudioStreamPlayer3D player) {

        bool end = false;

        void f() {        
            end = true;
            player.Finished -= f;
        }

        player.Finished += f;

        while(!end) {
			yield return MEC.Timing.WaitForOneFrame;
		}
    }

    public static IEnumerator<double> AsCoroutine(this AnimationMixer anim) {

        bool end = false;

        void f(Godot.StringName str) {
            end = true;
            anim.AnimationFinished -= f;
        }

        anim.AnimationFinished += f;

        while(!end) {
			yield return MEC.Timing.WaitForOneFrame;
		}
    } 

}
