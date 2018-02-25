using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;
using SeatClientV3.Code;
using SeatClientV3.OperateResult;
using SeatManage.EnumType;

namespace SeatClientV3.ViewModel
{
    public class NoteUC_ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 备注
        /// </summary>
        private string _notes;
        /// <summary>
        /// 类型
        /// </summary>
        private OrnamentType _noteType;
        public SystemObject ClientObject
        {
            get { return SystemObject.GetInstance(); }
        }


        /// <summary>
        /// 类型
        /// </summary>
        public OrnamentType NoteType
        {
            get { return _noteType; }
            set
            {
                _noteType = value;
                OnPropertyChanged("NoteImage");
            }
        }

        public ImageBrush NoteImage
        {
            get
            {
                switch (_noteType)
                {
                    case OrnamentType.AirConditioning:
                        return
                            SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage)
                                .NoteAirConditioning;
                    case OrnamentType.Bookshelf:
                        return
                            SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage)
                                .NoteBookshelf;
                    case OrnamentType.Door:
                        return
                            SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage)
                                .NotenoteDoor;
                    case OrnamentType.Elevator:
                        return
                            SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage)
                                .NoteElevator;
                    case OrnamentType.PCTable:
                        return
                            SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage)
                                .NotePCTableg;
                    case OrnamentType.Pillar:
                        return
                            SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage)
                                .NotePillar;
                    case OrnamentType.Plant:
                        return
                            SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage)
                                .NotePlant;
                    case OrnamentType.Roundtable:
                        return
                            SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage)
                                .NoteRoundtable;
                    case OrnamentType.Stairway:
                        return
                            SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage)
                                .NoteStairway;
                    case OrnamentType.Steps:
                        return
                            SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage)
                                .NoteSteps;
                    case OrnamentType.Table:
                        return
                            SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage)
                                .NoteTable;
                    case OrnamentType.Window:
                        return
                            SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage)
                                .NoteWindow;
                    case OrnamentType.Wall:
                        return
                            SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage)
                                .NoteWall;
                    default: return
                          SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage)
                              .Noteblank;
                }

            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged("Notes");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
