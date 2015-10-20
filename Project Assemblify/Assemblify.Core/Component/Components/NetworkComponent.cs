using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Assemblify.Core
{
    public class NetworkComponent : Component
    {
        private const int maxTransformStateBufferLength = 20, interpolationTimeDelta = 100;
        private const float interpolationBacktime = 0.1f;

        public StateSyncMode stateSyncMode = StateSyncMode.Interpolate;
        //public Guid playerOwnerGuid;

        private TransformComponent transform;

        // State sync
        private TransformState[] transformStateBuffer;
        private int transformStateBufferLength;

        public void PushStateToBuffer(float timestamp, Vector2 position, float rotation)
        {
            if (stateSyncMode == StateSyncMode.None)
            {
                transform.Position = position;
                transform.Rotation = rotation;
            }
            else
            {
                transformStateBuffer[0] = new TransformState(timestamp, position, rotation);
                transformStateBufferLength = Math.Min(transformStateBufferLength + 1, maxTransformStateBufferLength);

                // Really check for incorrect state timestamps every time a new state is pushed to the buffer?
                var incorrectStateTimestampsCount = 0;
                for (int i = 0; i < transformStateBufferLength - 1; i++)
                {
                    if (transformStateBuffer[i].timestamp < transformStateBuffer[i + 1].timestamp)
                    {
                        incorrectStateTimestampsCount++;
                    }
                }

                if (incorrectStateTimestampsCount > 1)
                {
                    Debug.Log("At least two states have incorrect timestamps, reshuffling...");
                    transformStateBuffer.OrderBy(ts => ts.timestamp); // Really reshuffle the states like this, or opposite?
                }
            }
        }

        protected override void OnCreate()
        {
            transform = parentActor.Transform;
            transformStateBuffer = new TransformState[maxTransformStateBufferLength];

            base.OnCreate();
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            switch (stateSyncMode)
            {
                case StateSyncMode.Interpolate:
                    InterpolateFromTransformStateBuffer(gameTime);
                    break;
                case StateSyncMode.Extrapolate:
                    ExtrapolateFromTransformStateBuffer(gameTime);
                    break;
            }

            base.OnUpdate(gameTime);
        }

        // Interpolate
        private void InterpolateFromTransformStateBuffer(GameTime gameTime)
        {
            float interpolationTime = (float)gameTime.TotalGameTime.TotalSeconds - interpolationBacktime;
            if (transformStateBufferLength == 0)
                return;

            if (transformStateBuffer[0].timestamp > interpolationTime)
            {
                for (int i = 0; i < transformStateBufferLength; i++)
                {
                    if (transformStateBuffer[i].timestamp <= interpolationTime || i == transformStateBufferLength)
                    {
                        var newerState = transformStateBuffer[Math.Min(i - 1, 0)];
                        var bestState = transformStateBuffer[i];

                        float length = newerState.timestamp - bestState.timestamp;
                        float time = 0.0f;

                        if (length > 0.0001f)
                            time = ((interpolationTime - bestState.timestamp) / length);

                        transform.Position = Vector2.Lerp(bestState.position, newerState.position, time);
                        transform.Rotation = MathHelper.Lerp(bestState.rotation, newerState.rotation, time);

                        break;
                    }
                }
            }
        }
        // Extrapolate
        private void ExtrapolateFromTransformStateBuffer(GameTime gameTime)
        {
            // TODO: Implement extrapolation (french curve seems interesting)
            // x = (x2 - x1) / dt * t + x1
            // y = (y2 - y1) / dt * t + y1
        }

        private class TransformState
        {
            public readonly float timestamp;
            public readonly Vector2 position;
            public readonly float rotation;

            public TransformState(float timestamp, Vector2 position, float rotation)
            {
                this.timestamp = timestamp;
                this.position = position;
                this.rotation = rotation;
            }
        }
    }

    public enum StateSyncMode
    {
        Interpolate,
        Extrapolate,
        None
    }
}
