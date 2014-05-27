using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

/// \mainpage
/// @author Tobias Karth
/// 
/// MotionDetection ist ein Teil der Projektarbeit der KTSI. Dieses Modul erkennt anhand der Kinect for Windows eine Person und liefert diverse Markante Körperpunkte in einem 3D Koordinatensystem.
/// API
/// ==================
/// SkeletonTracker
/// -------
/// Beschreibung | Methode
/// ------ | -------
/// Starten der Personenerfassung mit der Kinect. | Start
/// Schaltet die Personenerfassung ab. | Stop
/// \n
/// 
/// Body
/// -------
/// Beschreibung | Methode/Attribut
/// ------ | -------
/// Koordinatenobjekt des Punktes: Fussgelenk links | AnkleLeft
/// Koordinatenobjekt des Punktes: Fussgelenk rechts | AnkleRight
/// Koordinatenobjekt des Punktes: Ellenbogen links | ElbowLeft 
/// Koordinatenobjekt des Punktes: Ellenbogen rechts | ElbowRight 
/// Koordinatenobjekt des Punktes: Fuss links | FootLeft 
/// Koordinatenobjekt des Punktes: Fuss rechts | FootRight 
/// Koordinatenobjekt des Punktes: Hand links | HandLeft
/// Koordinatenobjekt des Punktes: Hand rechts | HandRight 
/// Koordinatenobjekt des Punktes: Kopf | Head
/// Koordinatenobjekt des Punktes: Hüfte mitte | HipCenter
/// Koordinatenobjekt des Punktes: Hüfte links | HipLeft
/// Koordinatenobjekt des Punktes: Hüfte rechts | HipRight
/// Koordinatenobjekt des Punktes: Knie links | KneeLeft
/// Koordinatenobjekt des Punktes: Knie rechts | KneeRight 
/// Koordinatenobjekt des Punktes: Schulter mitte | ShoulderCenter 
/// Koordinatenobjekt des Punktes: Schulter links | ShoulderLeft 
/// Koordinatenobjekt des Punktes: Schulter rechts | ShoulderRight 
/// Koordinatenobjekt des Punktes: Rückgrat | Spine 
/// Koordinatenobjekt des Punktes: Handgelenk links | WristLeft
/// Koordinatenobjekt des Punktes: Handgelenk rechts | WristRight 
/// X Koordinate des Körpers | X
/// Y Koordinate des Körpers  | Y
/// Z Koordinate des Körpers | Z
/// Flag, ob eine Person aktuell erkennt wird. Erfolgt 2 Sekundenlang kein Signal, so wird das Flag auf false gesetzt.  | IsTracked 
/// Instanz der Klasse | Instance 
/// Modifiziert alle Körperpunkte entlang der X-Achse um den Modifikator. | XModifikator 
/// Modifiziert alle Körperpunkte entlang der Y-Achse um den Modifikator. | YModifikator 
/// Modifiziert alle Körperpunkte entlang der Z-Achse um den Modifikator. | ZModifikator 
/// Skalliert den Getrackten Körper um den Skalierwert.  | Scale
/// \n
namespace MotionDetection
{
    /// <summary>  
    /// Ansteuerung der Kinect und speichert die getrackten Daten in das Body Objekt.
    /// </summary>
    public class SkeletonTracker
    {
        /// <summary>Kinect Sensor Objekt</summary>
        private KinectSensor sensor = null;
        /// <summary>Body Objekt für den zwischenspeicher</summary>
        private Body body = null;

        /// <summary>
        /// Starten der Personenerfassung mit der Kinect.
        /// </summary>
        public void Start()
        {
            sensor = KinectSensor.KinectSensors[0];
            sensor.ColorStream.Enable();
            sensor.SkeletonStream.Enable();

            sensor.SkeletonFrameReady += runtime_SkeletonFrameReady;
            sensor.Start();
        }

        /// <summary>
        /// Schaltet die Personenerfassung ab.
        /// </summary>
        public void Stop()
        {
            sensor.Stop();
        }

        /// <summary>
        /// Speichert pro erfasstes Frame die Körperdaten ins Body Objekt.
        /// </summary>
        /// <param name="sender">Senderobjekt</param>
        /// <param name="e">Eventargument</param>
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
                            if (skel.TrackingState != SkeletonTrackingState.NotTracked)
                            {
                                // Person ist erkannt
                                body.IsTracked = true;
                                // Position des Körpers
                                body.X = skel.Position.X;
                                body.Y = skel.Position.Y;
                                body.Z = skel.Position.Z;

                                // Zwischenspeichern der einzelnen Körperpunkte
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
                            }
                            else
                            {
                                // Keine Person erkannt
                                body.IsTracked = false;
                            }
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Zwischenspeichern eines Körperpunktes
        /// </summary>
        /// <param name="joint">Körperpunkt</param>
        private void setPosition(Joint joint)
        {
            // Wenn der Körperpunkt getrackt oder vermutet wird
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