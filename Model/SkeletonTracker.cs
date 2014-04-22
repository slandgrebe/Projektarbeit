using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Model
{
    public delegate void SkeletonTrackerEvent();
    
    public class SkeletonTracker
    {
        KinectSensor sensor = null;
        Body body = null;

        public event SkeletonTrackerEvent SkeletonEvent;
        
        public void Start()
        {
            sensor = KinectSensor.KinectSensors[0];
            sensor.ColorStream.Enable();
            sensor.SkeletonStream.Enable();

            sensor.SkeletonFrameReady += runtime_SkeletonFrameReady;
            sensor.Start();
        }

        public void Stop()
        {
            sensor.Stop();
        }

        private void runtime_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = null;
            body = Body.Instance;

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    if (skeletons == null)
                    {
                        // Skelettarray zuweisen
                        skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    }

                    skeletonFrame.CopySkeletonDataTo(skeletons);

                    if (skeletons.Length != 0)
                    {
                        // Getrackte Körperpunkte zwischenspeichern
                        Parallel.ForEach(skeletons, skel =>
                        {
                            if (skel.Joints[JointType.Spine].TrackingState == JointTrackingState.NotTracked)
                            {
                                body.IsTracked = false;
                            }
                            else
                            {
                                body.IsTracked = true;
                            }
                            setPosition(skel.Joints[JointType.AnkleLeft]);
                            setPosition(skel.Joints[JointType.AnkleRight]);
                            setPosition(skel.Joints[JointType.ElbowLeft]);
                            setPosition(skel.Joints[JointType.ElbowRight]);
                            setPosition(skel.Joints[JointType.FootLeft]);
                            setPosition(skel.Joints[JointType.FootRight]);
                            setPosition(skel.Joints[JointType.HandLeft]);
                            setPosition(skel.Joints[JointType.HandRight]);
                            setPosition(skel.Joints[JointType.Head]);
                            setPosition(skel.Joints[JointType.HipCenter]);
                            setPosition(skel.Joints[JointType.HipLeft]);
                            setPosition(skel.Joints[JointType.HipRight]);
                            setPosition(skel.Joints[JointType.KneeLeft]);
                            setPosition(skel.Joints[JointType.KneeRight]);
                            setPosition(skel.Joints[JointType.ShoulderCenter]);
                            setPosition(skel.Joints[JointType.ShoulderLeft]);
                            setPosition(skel.Joints[JointType.ShoulderRight]);
                            setPosition(skel.Joints[JointType.Spine]);
                            setPosition(skel.Joints[JointType.WristLeft]);
                            setPosition(skel.Joints[JointType.WristRight]);
                        });
                        if (SkeletonEvent != null)
                            SkeletonEvent();
                    }
                }
            }
        }

        

        private void setPosition(Joint joint)
        {
            if (joint.TrackingState == JointTrackingState.Tracked || joint.TrackingState == JointTrackingState.Inferred)
            {
                switch (joint.JointType)
                {
                    case JointType.AnkleLeft:
                        body.AnkleLeft.X = joint.Position.X;
                        body.AnkleLeft.Y = joint.Position.Y;
                        body.AnkleLeft.Z = joint.Position.Z;
                        break;
                    case JointType.AnkleRight:
                        body.AnkleRight.X = joint.Position.X;
                        body.AnkleRight.Y = joint.Position.Y;
                        body.AnkleRight.Z = joint.Position.Z;
                        break;
                    case JointType.ElbowLeft:
                        body.ElbowLeft.X = joint.Position.X;
                        body.ElbowLeft.Y = joint.Position.Y;
                        body.ElbowLeft.Z = joint.Position.Z;
                        break;
                    case JointType.ElbowRight:
                        body.ElbowRight.X = joint.Position.X;
                        body.ElbowRight.Y = joint.Position.Y;
                        body.ElbowRight.Z = joint.Position.Z;
                        break;
                    case JointType.FootLeft:
                        body.FootLeft.X = joint.Position.X;
                        body.FootLeft.Y = joint.Position.Y;
                        body.FootLeft.Z = joint.Position.Z;
                        break;
                    case JointType.FootRight:
                        body.FootRight.X = joint.Position.X;
                        body.FootRight.Y = joint.Position.Y;
                        body.FootRight.Z = joint.Position.Z;
                        break;
                    case JointType.HandRight:
                        body.HandRight.X = joint.Position.X;
                        body.HandRight.Y = joint.Position.Y;
                        body.HandRight.Z = joint.Position.Z;
                        break;
                    case JointType.HandLeft:
                        body.HandLeft.X = joint.Position.X;
                        body.HandLeft.Y = joint.Position.Y;
                        body.HandLeft.Z = joint.Position.Z;
                        break;
                    case JointType.Head:
                        body.Head.X = joint.Position.X;
                        body.Head.Y = joint.Position.Y;
                        body.Head.Z = joint.Position.Z;
                        break;
                    case JointType.HipCenter:
                        body.HipCenter.X = joint.Position.X;
                        body.HipCenter.Y = joint.Position.Y;
                        body.HipCenter.Z = joint.Position.Z;
                        break;
                    case JointType.HipLeft:
                        body.HipLeft.X = joint.Position.X;
                        body.HipLeft.Y = joint.Position.Y;
                        body.HipLeft.Z = joint.Position.Z;
                        break;
                    case JointType.HipRight:
                        body.HipRight.X = joint.Position.X;
                        body.HipRight.Y = joint.Position.Y;
                        body.HipRight.Z = joint.Position.Z;
                        break;
                    case JointType.KneeLeft:
                        body.KneeLeft.X = joint.Position.X;
                        body.KneeLeft.Y = joint.Position.Y;
                        body.KneeLeft.Z = joint.Position.Z;
                        break;
                    case JointType.KneeRight:
                        body.KneeRight.X = joint.Position.X;
                        body.KneeRight.Y = joint.Position.Y;
                        body.KneeRight.Z = joint.Position.Z;
                        break;
                    case JointType.ShoulderCenter:
                        body.ShoulderCenter.X = joint.Position.X;
                        body.ShoulderCenter.Y = joint.Position.Y;
                        body.ShoulderCenter.Z = joint.Position.Z;
                        break;
                    case JointType.ShoulderLeft:
                        body.ShoulderLeft.X = joint.Position.X;
                        body.ShoulderLeft.Y = joint.Position.Y;
                        body.ShoulderLeft.Z = joint.Position.Z;
                        break;
                    case JointType.ShoulderRight:
                        body.ShoulderRight.X = joint.Position.X;
                        body.ShoulderRight.Y = joint.Position.Y;
                        body.ShoulderRight.Z = joint.Position.Z;
                        break;
                    case JointType.Spine:
                        body.Spine.X = joint.Position.X;
                        body.Spine.Y = joint.Position.Y;
                        body.Spine.Z = joint.Position.Z;
                        break;
                    case JointType.WristLeft:
                        body.WristLeft.X = joint.Position.X;
                        body.WristLeft.Y = joint.Position.Y;
                        body.WristLeft.Z = joint.Position.Z;
                        break;
                    case JointType.WristRight:
                        body.WristRight.X = joint.Position.X;
                        body.WristRight.Y = joint.Position.Y;
                        body.WristRight.Z = joint.Position.Z;
                        break;
                }
            }
        }
    }
}
