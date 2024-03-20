using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static DonjonImporter.DDClass;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace DonjonImporter
{
    public partial class Form1 : Form
    {



        List<Vector2> corridor = new List<Vector2>();
        Dictionary<Vector2, bool> visitedcorridors = new Dictionary<Vector2, bool>();
        List<List<Vector2>> corridorturn = new List<List<Vector2>>();
        Dictionary<string, List<Vector2>> doors = new Dictionary<string, List<Vector2>>();
        List<Vector2> doorlist = new List<Vector2>();
        Dictionary<Vector2, bool> connectedoors = new Dictionary<Vector2, bool>();

        Dictionary<Vector2, bool> eastdoor = new Dictionary<Vector2, bool>();
        Dictionary<Vector2, bool> southdoor = new Dictionary<Vector2, bool>();
        Dictionary<Vector2, bool> westdoor = new Dictionary<Vector2, bool>();
        Dictionary<Vector2, bool> northdoor = new Dictionary<Vector2, bool>();
        Root json;
        int recursions = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            corridor = new List<Vector2>();
            visitedcorridors = new Dictionary<Vector2, bool>();
            corridorturn = new List<List<Vector2>>();
            doors = new Dictionary<string, List<Vector2>>();
            doorlist = new List<Vector2>();
            connectedoors = new Dictionary<Vector2, bool>();

            eastdoor = new Dictionary<Vector2, bool>();
            southdoor = new Dictionary<Vector2, bool>();
            westdoor = new Dictionary<Vector2, bool>();
            northdoor = new Dictionary<Vector2, bool>();

            recursions = 0;

            json = new Root();

            var file = openFileDialog1.ShowDialog();

            if (file == DialogResult.OK)
            {
                txt_file_path.Text = openFileDialog1.FileName;

                json = JsonConvert.DeserializeObject<Root>(File.ReadAllText(txt_file_path.Text));

                lbl_map_width.Text = json.settings.n_cols.ToString();
                lbl_map_height.Text = json.settings.n_rows.ToString();
                lbl_map_cell_count.Text = (json.settings.n_rows * json.settings.n_cols).ToString();
                lbl_rooms.Text = (json.rooms.Count - 1).ToString();

            }
        }

        public void Pathfinding(int x, int y, string direction, List<List<int>> map, string doordirection)
        {
            visitedcorridors[new Vector2(x, y)] = true;

            bool up, right, down, left;
            recursions++;



            up = false;

            if (visitedcorridors.ContainsKey(new Vector2(x, y - 1)) && y > 0 && !visitedcorridors[new Vector2(x, y - 1)])
            {
                if (!southdoor.ContainsKey(new Vector2(x, y - 1)))
                {
                    if (direction != "up" && direction != "")
                    {
                        if (direction == "right" && corridor.Count > 0)
                        {

                            bool adjright, adjdown;
                            adjright = false;
                            adjdown = false;
                            if (visitedcorridors.ContainsKey(new Vector2(x + 1, y)) && y > 0 && !visitedcorridors[new Vector2(x + 1, y)])
                                adjright = true;
                            if (visitedcorridors.ContainsKey(new Vector2(x, y + 1)) && y > 0 && !visitedcorridors[new Vector2(x, y + 1)])
                                adjdown = true;
                            if (adjright && adjdown)
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));

                                corridorturn.Add(list);

                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjright)
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));

                                corridorturn.Add(list);

                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjdown)
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));

                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));
                                closer.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                                corridorturn.Add(closer);

                                closer = new List<Vector2>();
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));

                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));
                                closer.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                                corridorturn.Add(closer);

                                closer = new List<Vector2>();
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }


                        }
                        else if (direction == "left" && corridor.Count > 0)
                        {

                            bool adjleft, adjdown;
                            adjleft = false;
                            adjdown = false;
                            if (visitedcorridors.ContainsKey(new Vector2(x - 1, y)) && y > 0 && !visitedcorridors[new Vector2(x - 1, y)])
                                adjleft = true;
                            if (visitedcorridors.ContainsKey(new Vector2(x, y + 1)) && y > 0 && !visitedcorridors[new Vector2(x, y + 1)])
                                adjdown = true;
                            if (adjleft && adjdown)
                            {
                                corridor.Add(new Vector2((x + 1) * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));

                                corridorturn.Add(list);
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjleft)
                            {
                                corridor.Add(new Vector2((x + 1) * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X - 256, corridor[1].Y + 256));

                                corridorturn.Add(list);

                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjdown)
                            {
                                corridor.Add(new Vector2((x + 1) * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));

                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X - 256, corridor[1].Y));
                                closer.Add(new Vector2(corridor[1].X - 256, corridor[1].Y + 256));
                                corridorturn.Add(closer);

                                closer = new List<Vector2>();
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else
                            {
                                corridor.Add(new Vector2((x + 1) * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X - 256, corridor[1].Y + 256));

                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X - 256, corridor[1].Y));
                                closer.Add(new Vector2(corridor[1].X - 256, corridor[1].Y + 256));
                                corridorturn.Add(closer);

                                closer = new List<Vector2>();
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }


                        }


                        corridor.Add(new Vector2(x * 256, y * 256));
                    }
                    else if (corridor.Count > 0)
                    {
                        bool adjright1, adjleft1;
                        adjright1 = false;
                        adjleft1 = false;
                        if (visitedcorridors.ContainsKey(new Vector2(x + 1, y)) && y > 0 && !visitedcorridors[new Vector2(x + 1, y)])
                            adjright1 = true;
                        if (visitedcorridors.ContainsKey(new Vector2(x - 1, y)) && y > 0 && !visitedcorridors[new Vector2(x - 1, y)])
                            adjleft1 = true;


                        if (adjright1 && adjleft1)
                        {
                            corridor.Add(new Vector2(x * 256, y * 256));
                            corridorturn.Add(corridor);
                            List<Vector2> list = new List<Vector2>();

                            list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                            list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                            corridorturn.Add(list);
                            list = new List<Vector2>();
                            corridor = new List<Vector2>();
                        }
                        else if (adjright1)
                        {
                            corridor.Add(new Vector2(x * 256, y * 256));
                            corridorturn.Add(corridor);
                            List<Vector2> list = new List<Vector2>();

                            list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                            list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                            corridorturn.Add(list);


                            list = new List<Vector2>();
                            corridor = new List<Vector2>();
                            corridor.Add(new Vector2(x * 256, y * 256));

                        }
                        else if (adjleft1)
                        {
                            corridor.Add(new Vector2(x * 256, y * 256));
                            corridorturn.Add(corridor);
                            List<Vector2> list = new List<Vector2>();

                            list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                            list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                            corridorturn.Add(list);

                            list = new List<Vector2>();
                            corridor = new List<Vector2>();
                            corridor.Add(new Vector2(x * 256, (y + 1) * 256));
                        }
                    }
                    direction = "up";
                    Pathfinding(x, y - 1, direction, map, doordirection);
                    up = true;
                }

            }
            right = false;
            if (visitedcorridors.ContainsKey(new Vector2(x + 1, y)) && !visitedcorridors[new Vector2(x + 1, y)])
            {
                if (!westdoor.ContainsKey(new Vector2(x + 1, y)))
                {
                    if (direction != "right" && direction != "")
                    {
                        if (direction == "up" && corridor.Count > 0)
                        {
                            bool adjup, adjleft;
                            adjup = false;
                            adjleft = false;
                            if (visitedcorridors.ContainsKey(new Vector2(x, y - 1)) && y > 0 && !visitedcorridors[new Vector2(x, y - 1)])
                                adjup = true;
                            if (visitedcorridors.ContainsKey(new Vector2(x - 1, y)) && y > 0 && !visitedcorridors[new Vector2(x - 1, y)])
                                adjleft = true;

                            if (adjup && adjleft)
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                                corridorturn.Add(list);

                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjup)
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                                corridorturn.Add(list);

                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjleft)
                            {
                                corridor.Add(new Vector2(x * 256, (y + 1) * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));
                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y - 256));
                                closer.Add(new Vector2(corridor[1].X + 256, corridor[1].Y - 256));
                                corridorturn.Add(closer);


                                closer = new List<Vector2>();
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y));
                                closer.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));
                                corridorturn.Add(closer);


                                closer = new List<Vector2>();
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }

                        }
                        else if (direction == "down" && corridor.Count > 0)
                        {
                            bool adjleft, adjdown;
                            adjleft = false;
                            adjdown = false;
                            if (visitedcorridors.ContainsKey(new Vector2(x - 1, y)) && y > 0 && !visitedcorridors[new Vector2(x - 1, y)])
                                adjleft = true;
                            if (visitedcorridors.ContainsKey(new Vector2(x, y + 1)) && y > 0 && !visitedcorridors[new Vector2(x, y + 1)])
                                adjdown = true;

                            if (adjleft && adjdown)
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));
                                corridorturn.Add(list);

                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjleft)
                            {

                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));
                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));
                                closer.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                                corridorturn.Add(closer);

                                if (eastdoor.ContainsKey(new Vector2(x - 1, y)) && !eastdoor[new Vector2(x - 1, y)])
                                {
                                    closer = new List<Vector2>();
                                    closer.Add(new Vector2(corridor[1].X, corridor[1].Y));
                                    closer.Add(new Vector2(corridor[1].X - 256, corridor[1].Y));
                                    corridorturn.Add(closer);
                                    closer = new List<Vector2>();
                                    closer.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));
                                    closer.Add(new Vector2(corridor[1].X - 256, corridor[1].Y + 256));
                                    corridorturn.Add(closer);
                                }
                                closer = new List<Vector2>();

                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjdown)
                            {
                                corridor.Add(new Vector2(x * 256, (y + 1) * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y - 256));
                                corridorturn.Add(list);

                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else
                            {
                                corridor.Add(new Vector2(x * 256, (y + 1) * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y - 256));
                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y));
                                closer.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));
                                corridorturn.Add(closer);


                                closer = new List<Vector2>();
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }



                        }

                        corridor.Add(new Vector2((x + 1) * 256, y * 256));

                    }
                    else if (corridor.Count > 0)
                    {
                        bool adjup1, adjdown1;
                        adjup1 = false;
                        adjdown1 = false;
                        if (visitedcorridors.ContainsKey(new Vector2(x, y - 1)) && y > 0 && !visitedcorridors[new Vector2(x, y - 1)])
                            adjup1 = true;
                        if (visitedcorridors.ContainsKey(new Vector2(x, y + 1)) && y > 0 && !visitedcorridors[new Vector2(x, y + 1)])
                            adjdown1 = true;


                        if (adjup1 && adjdown1)
                        {
                            corridor.Add(new Vector2(x * 256, y * 256));
                            corridorturn.Add(corridor);
                            List<Vector2> list = new List<Vector2>();

                            list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                            list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                            corridorturn.Add(list);
                            list = new List<Vector2>();
                            corridor = new List<Vector2>();
                        }
                        else if (adjup1)
                        {
                            corridor.Add(new Vector2(x * 256, y * 256));
                            corridorturn.Add(corridor);
                            List<Vector2> list = new List<Vector2>();

                            list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                            list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                            corridorturn.Add(list);


                            list = new List<Vector2>();
                            corridor = new List<Vector2>();
                            corridor.Add(new Vector2(x * 256, y * 256));

                        }
                        else if (adjdown1)
                        {
                            corridor.Add(new Vector2((x + 1) * 256, y * 256));
                            corridorturn.Add(corridor);
                            List<Vector2> list = new List<Vector2>();

                            list.Add(new Vector2(corridor[0].X - 256, corridor[0].Y + 256));
                            list.Add(new Vector2(corridor[1].X - 256, corridor[1].Y + 256));
                            corridorturn.Add(list);

                            list = new List<Vector2>();
                            corridor = new List<Vector2>();
                            corridor.Add(new Vector2((x + 1) * 256, y * 256));
                        }
                    }
                    direction = "right";
                    Pathfinding(x + 1, y, direction, map, doordirection);
                    right = true;
                }

            }
            down = false;
            if (visitedcorridors.ContainsKey(new Vector2(x, y + 1)) && !visitedcorridors[new Vector2(x, y + 1)])
            {
                if (!northdoor.ContainsKey(new Vector2(x, y + 1)))
                {
                    if (direction != "down" && direction != "")
                    {

                        if (direction == "right" && corridor.Count > 0)
                        {
                            bool adjright, adjup;
                            adjright = false;
                            adjup = false;
                            if (visitedcorridors.ContainsKey(new Vector2(x + 1, y)) && y > 0 && !visitedcorridors[new Vector2(x + 1, y)])
                                adjright = true;
                            if (visitedcorridors.ContainsKey(new Vector2(x, y - 1)) && y > 0 && !visitedcorridors[new Vector2(x, y - 1)])
                                adjup = true;

                            if (adjright && adjup)
                            {
                                corridor.Add(new Vector2((x + 1) * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X - 256, corridor[0].Y + 256));
                                corridorturn.Add(list);

                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjright)
                            {
                                corridor.Add(new Vector2((x + 1) * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X - 256, corridor[0].Y + 256));
                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y));
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));
                                corridorturn.Add(closer);

                                closer = new List<Vector2>();
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjup)
                            {
                                corridor.Add(new Vector2((x + 1) * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X - 256, corridor[0].Y + 256));
                                corridorturn.Add(list);

                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else
                            {
                                corridor.Add(new Vector2((x + 1) * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X - 256, corridor[0].Y + 256));
                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y));
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));
                                corridorturn.Add(closer);

                                closer = new List<Vector2>();
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }

                        }
                        else if (direction == "left" && corridor.Count > 0)
                        {
                            bool adjleft, adjup;
                            adjleft = false;
                            adjup = false;
                            if (visitedcorridors.ContainsKey(new Vector2(x - 1, y)) && y > 0 && !visitedcorridors[new Vector2(x - 1, y)])
                                adjleft = true;
                            if (visitedcorridors.ContainsKey(new Vector2(x, y - 1)) && y > 0 && !visitedcorridors[new Vector2(x, y - 1)])
                                adjup = true;

                            if (adjleft && adjup)
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));

                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));
                                corridorturn.Add(list);

                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjleft)
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));

                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                                corridorturn.Add(list);

                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjup)
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));

                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));
                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y));
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));
                                corridorturn.Add(closer);

                                closer = new List<Vector2>();
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));

                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();
                                list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y));
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));
                                corridorturn.Add(closer);

                                closer = new List<Vector2>();
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }

                        }



                        corridor.Add(new Vector2(x * 256, (y + 1) * 256));
                    }
                    else if (corridor.Count > 0)
                    {
                        bool adjright1, adjleft1;
                        adjright1 = false;
                        adjleft1 = false;
                        if (visitedcorridors.ContainsKey(new Vector2(x + 1, y)) && y > 0 && !visitedcorridors[new Vector2(x + 1, y)])
                            adjright1 = true;
                        if (visitedcorridors.ContainsKey(new Vector2(x - 1, y)) && y > 0 && !visitedcorridors[new Vector2(x - 1, y)])
                            adjleft1 = true;


                        if (adjright1 && adjleft1)
                        {
                            corridor.Add(new Vector2(x * 256, y * 256));
                            corridorturn.Add(corridor);
                            List<Vector2> list = new List<Vector2>();

                            list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                            list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                            corridorturn.Add(list);
                            list = new List<Vector2>();
                            corridor = new List<Vector2>();
                        }
                        else if (adjright1)
                        {
                            corridor.Add(new Vector2(x * 256, (y + 1) * 256));
                            corridorturn.Add(corridor);
                            List<Vector2> list = new List<Vector2>();

                            list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                            list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y - 256));
                            corridorturn.Add(list);


                            list = new List<Vector2>();
                            corridor = new List<Vector2>();

                        }
                        else if (adjleft1)
                        {
                            corridor.Add(new Vector2(x * 256, y * 256));
                            corridorturn.Add(corridor);
                            List<Vector2> list = new List<Vector2>();

                            list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                            list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                            corridorturn.Add(list);

                            list = new List<Vector2>();
                            corridor = new List<Vector2>();
                            corridor.Add(new Vector2(x * 256, (y + 1) * 256));
                        }
                    }
                    direction = "down";
                    Pathfinding(x, y + 1, direction, map, doordirection);
                    down = true;
                }

            }
            left = false;
            if (visitedcorridors.ContainsKey(new Vector2(x - 1, y)) && x > 0 && !visitedcorridors[new Vector2(x - 1, y)])
            {
                if (!eastdoor.ContainsKey(new Vector2(x - 1, y)))
                {
                    if (direction != "left" && direction != "")
                    {
                        if (direction == "down" && !up && corridor.Count > 0)
                        {
                            bool adjright, adjup;
                            adjright = false;
                            adjup = false;

                            if (visitedcorridors.ContainsKey(new Vector2(x + 1, y)) && y > 0 && !visitedcorridors[new Vector2(x + 1, y)])
                                adjright = true;
                            if (visitedcorridors.ContainsKey(new Vector2(x, y - 1)) && y > 0 && !visitedcorridors[new Vector2(x, y - 1)])
                                adjup = true;
                            if (adjright && adjup)
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                                corridorturn.Add(list);

                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjright)
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));
                                closer.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                                corridorturn.Add(closer);


                                closer = new List<Vector2>();
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjup)
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                                corridorturn.Add(list);

                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));
                                closer.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));
                                corridorturn.Add(closer);


                                closer = new List<Vector2>();
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }

                        }
                        else if (direction == "up" && corridor.Count > 0)
                        {
                            bool adjright, adjup;
                            adjright = false;
                            adjup = false;
                            if (visitedcorridors.ContainsKey(new Vector2(x + 1, y)) && y > 0 && !visitedcorridors[new Vector2(x + 1, y)])
                                adjright = true;
                            if (visitedcorridors.ContainsKey(new Vector2(x, y - 1)) && y > 0 && !visitedcorridors[new Vector2(x, y - 1)])
                                adjup = true;

                            if (adjright && adjup)
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));
                                corridorturn.Add(list);

                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjright)
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));
                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y));
                                closer.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));
                                corridorturn.Add(closer);


                                closer = new List<Vector2>();
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else if (adjup)
                            {
                                corridor.Add(new Vector2(x * 256, y * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));
                                corridorturn.Add(list);
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }
                            else
                            {
                                corridor.Add(new Vector2(x * 256, (y + 1) * 256));
                                corridorturn.Add(corridor);
                                List<Vector2> list = new List<Vector2>();

                                list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                                list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y - 256));
                                corridorturn.Add(list);
                                List<Vector2> closer = new List<Vector2>();
                                closer.Add(new Vector2(corridor[1].X, corridor[1].Y - 256));
                                closer.Add(new Vector2(corridor[1].X + 256, corridor[1].Y - 256));
                                corridorturn.Add(closer);


                                closer = new List<Vector2>();
                                list = new List<Vector2>();
                                corridor = new List<Vector2>();
                            }


                        }
                        corridor.Add(new Vector2(x * 256, y * 256));
                    }
                    direction = "left";
                    Pathfinding(x - 1, y, direction, map, doordirection);
                    left = true;
                }

            }



            int pathopen = 0;

            if (up)
                pathopen++;
            if (right)
                pathopen++;
            if (down)
                pathopen++;
            if (left)
                pathopen++;


            //if end of the line 
            if (pathopen == 0)
            {
                if (direction == "")
                {
                    //check if door to door connection is verical or horizontal
                    //if up is void
                    if (doordirection == "up" && southdoor.ContainsKey(new Vector2(x, y - 1)) && !southdoor[new Vector2(x, y - 1)])
                    {
                        List<Vector2> list = new List<Vector2>();
                        corridor.Add(new Vector2(corridor[0].X, corridor[0].Y - 256));
                        corridorturn.Add(corridor);

                        list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                        list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));
                        corridorturn.Add(list);
                        corridor = new List<Vector2>();
                        connectedoors[new Vector2(x, y + 1)] = true;
                        southdoor[new Vector2(x, y - 1)] = true;
                        visitedcorridors[new Vector2(x, y + 1)] = true;
                    }
                    else if (doordirection == "right" && westdoor.ContainsKey(new Vector2(x + 1, y)) && !westdoor[new Vector2(x + 1, y)])
                    {
                        List<Vector2> list = new List<Vector2>();
                        corridor.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                        corridorturn.Add(corridor);

                        list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                        list.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));
                        corridorturn.Add(list);
                        corridor = new List<Vector2>();
                        connectedoors[new Vector2(x, y + 1)] = true;
                        westdoor[new Vector2(x + 1, y)] = true;
                        visitedcorridors[new Vector2(x, y + 1)] = true;
                    }
                    else if (doordirection == "down" && northdoor.ContainsKey(new Vector2(x, y + 1)) && !northdoor[new Vector2(x, y + 1)])
                    {
                        List<Vector2> list = new List<Vector2>();
                        corridor.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                        corridorturn.Add(corridor);

                        list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                        list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));
                        corridorturn.Add(list);
                        corridor = new List<Vector2>();
                        connectedoors[new Vector2(x, y + 1)] = true;
                        northdoor[new Vector2(x, y + 1)] = true;
                        visitedcorridors[new Vector2(x, y + 1)] = true;
                    }
                    else if (doordirection == "left" && eastdoor.ContainsKey(new Vector2(x - 1, y)) && !eastdoor[new Vector2(x - 1, y)])
                    {
                        List<Vector2> list = new List<Vector2>();
                        corridor.Add(new Vector2(corridor[0].X - 256, corridor[0].Y));
                        corridorturn.Add(corridor);

                        list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                        list.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));
                        corridorturn.Add(list);
                        corridor = new List<Vector2>();
                        connectedoors[new Vector2(x, y + 1)] = true;
                        eastdoor[new Vector2(x - 1, y)] = true;
                        visitedcorridors[new Vector2(x, y + 1)] = true;
                    }


                }
                if (direction == "up")
                {
                    List<Vector2> closer = new List<Vector2>();
                    List<Vector2> list = new List<Vector2>();

                    if (eastdoor.ContainsKey(new Vector2(x - 1, y)))
                    {
                        corridor.Add(new Vector2(x * 256, (y + 1) * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                        list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y - 256));

                        closer.Add(new Vector2(list[1].X, list[1].Y));
                        closer.Add(new Vector2(list[1].X - 512, list[1].Y));
                        corridorturn.Add(list);
                        corridorturn.Add(closer);
                        closer = new List<Vector2>();

                        closer.Add(new Vector2(corridor[1].X, corridor[1].Y));
                        closer.Add(new Vector2(corridor[1].X - 256, corridor[1].Y));
                        corridorturn.Add(closer);
                        list = new List<Vector2>();
                        list = new List<Vector2>();
                        closer = new List<Vector2>();
                        eastdoor[new Vector2(x - 1, y)] = true;
                        visitedcorridors[new Vector2(x - 1, y)] = true;
                    }
                    else if (westdoor.ContainsKey(new Vector2(x + 1, y)))
                    {
                        corridor.Add(new Vector2(x * 256, (y - 1) * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                        list.Add(new Vector2((x + 1) * 256, (y - 1) * 256));
                        corridor = new List<Vector2>();
                        corridorturn.Add(list);
                        westdoor[new Vector2(x * 256, (y - 1) * 256)] = true;
                        visitedcorridors[new Vector2(x, y - 1)] = true;
                    }
                    else if (southdoor.ContainsKey(new Vector2(x, y - 1)))
                    {
                        corridor.Add(new Vector2(x * 256, (y - 1) * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                        list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));

                        corridorturn.Add(list);
                        corridor = new List<Vector2>();
                        list = new List<Vector2>();
                        southdoor[new Vector2(x, y - 1)] = true;
                        visitedcorridors[new Vector2(x, y - 1)] = true;
                    }
                    else
                    {
                        corridor.Add(new Vector2(x * 256, y * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                        list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));
                        closer.Add(new Vector2(corridor[1].X, corridor[1].Y));
                        closer.Add(new Vector2(corridor[1].X + 256, corridor[1].Y));
                        corridor = new List<Vector2>();
                        corridorturn.Add(list);
                        corridorturn.Add(closer);
                        visitedcorridors[new Vector2(x, y - 1)] = true;
                    }
                }
                if (direction == "down")
                {
                    List<Vector2> closer = new List<Vector2>();
                    List<Vector2> list = new List<Vector2>();

                    if (eastdoor.ContainsKey(new Vector2(x - 1, y)))
                    {
                        corridor.Add(new Vector2(x * 256, y * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                        list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));

                        corridorturn.Add(list);
                        closer.Add(new Vector2(list[1].X, list[1].Y));
                        closer.Add(new Vector2(list[1].X - 512, list[1].Y));
                        corridorturn.Add(list);
                        corridorturn.Add(closer);
                        closer = new List<Vector2>();
                        closer.Add(new Vector2(corridor[1].X, corridor[1].Y));
                        closer.Add(new Vector2(corridor[1].X - 256, corridor[1].Y));
                        corridorturn.Add(closer);

                        corridor = new List<Vector2>();
                        closer = new List<Vector2>();
                        list = new List<Vector2>();
                        eastdoor[new Vector2(x - 1, y)] = true;
                        visitedcorridors[new Vector2(x, y + 1)] = true;
                    }
                    else if (westdoor.ContainsKey(new Vector2(x + 1, y)))
                    {
                        corridor.Add(new Vector2(x * 256, (y + 1) * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                        list.Add(new Vector2((x + 1) * 256, (y + 1) * 256));
                        corridor = new List<Vector2>();
                        corridorturn.Add(list);
                        westdoor[new Vector2(x + 1, y)] = true;
                        visitedcorridors[new Vector2(x, y + 1)] = true;
                    }
                    else if (northdoor.ContainsKey(new Vector2(x, y + 1)))
                    {
                        corridor.Add(new Vector2(x * 256, (y + 1) * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                        list.Add(new Vector2((x + 1) * 256, (y + 1) * 256));
                        corridor = new List<Vector2>();
                        corridorturn.Add(list);
                        northdoor[new Vector2(x, y + 1)] = true;
                        visitedcorridors[new Vector2(x, y + 1)] = true;

                    }
                    else
                    {
                        corridor.Add(new Vector2(x * 256, (y + 1) * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X + 256, corridor[0].Y));
                        list.Add(new Vector2((x + 1) * 256, (y + 1) * 256));
                        closer.Add(new Vector2(corridor[0].X, (y + 1) * 256));
                        closer.Add(new Vector2(corridor[0].X + 256, (y + 1) * 256));
                        corridor = new List<Vector2>();
                        corridorturn.Add(list);
                        corridorturn.Add(closer);
                        visitedcorridors[new Vector2(x, y + 1)] = true;
                    }
                }
                if (direction == "right")
                {
                    //if the wall resolves into a door
                    List<Vector2> closer = new List<Vector2>();
                    List<Vector2> list = new List<Vector2>();


                    if (westdoor.ContainsKey(new Vector2(x + 1, y)))
                    {
                        corridor.Add(new Vector2((x + 1) * 256, y * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                        list.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));
                        corridor = new List<Vector2>();
                        corridorturn.Add(list);
                        westdoor[new Vector2(x + 1, y)] = true;
                        visitedcorridors[new Vector2(x + 1, y)] = true;
                    }
                    else if (northdoor.ContainsKey(new Vector2(x, y + 1)))
                    {
                        corridor.Add(new Vector2((x + 1) * 256, y * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                        list.Add(new Vector2((x + 1) * 256, (y + 1) * 256));
                        corridor = new List<Vector2>();
                        corridorturn.Add(list);
                        northdoor[new Vector2(x, y + 1)] = true;
                        visitedcorridors[new Vector2(x + 1, y)] = true;
                    }
                    else if (southdoor.ContainsKey(new Vector2(x, y - 1)))
                    {
                        corridor.Add(new Vector2(x * 256, y * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                        list.Add(new Vector2(corridor[1].X + 256, corridor[1].Y + 256));

                        corridorturn.Add(list);
                        closer.Add(new Vector2(list[1].X, list[1].Y));
                        closer.Add(new Vector2(list[1].X, list[1].Y - 512));
                        corridorturn.Add(closer);

                        closer = new List<Vector2>();
                        closer.Add(new Vector2(corridor[1].X, corridor[1].Y));
                        closer.Add(new Vector2(corridor[1].X, corridor[1].Y - 256));
                        corridorturn.Add(closer);
                        closer = new List<Vector2>();
                        corridor = new List<Vector2>();
                        list = new List<Vector2>();
                        southdoor[new Vector2(x, y - 1)] = true;
                        visitedcorridors[new Vector2(x, y - 1)] = true;
                    }
                    else
                    {
                        corridor.Add(new Vector2((x + 1) * 256, y * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                        list.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));

                        corridorturn.Add(list);
                        closer.Add(new Vector2(corridor[1].X, corridor[1].Y));
                        closer.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));
                        corridorturn.Add(closer);
                        corridor = new List<Vector2>();
                        closer = new List<Vector2>();
                        list = new List<Vector2>();
                        visitedcorridors[new Vector2(x + 1, y)] = true;
                    }

                }
                if (direction == "left")
                {
                    //if the wall resolves into a door
                    List<Vector2> closer = new List<Vector2>();
                    List<Vector2> list = new List<Vector2>();

                    if (eastdoor.ContainsKey(new Vector2(x - 1, y)))
                    {
                        corridor.Add(new Vector2((x - 1) * 256, y * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                        list.Add(new Vector2((x - 1) * 256, (y + 1) * 256));
                        corridor = new List<Vector2>();
                        corridorturn.Add(list);
                        eastdoor[new Vector2(x - 1, y)] = true;
                        visitedcorridors[new Vector2(x - 1, y)] = true;
                    }
                    else if (northdoor.ContainsKey(new Vector2(x, y + 1)))
                    {
                        corridor.Add(new Vector2((x - 1) * 256, y * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                        list.Add(new Vector2((x - 1) * 256, (y + 1) * 256));
                        corridor = new List<Vector2>();
                        corridorturn.Add(list);
                        northdoor[new Vector2(x, y + 1)] = true;
                        visitedcorridors[new Vector2(x - 1, y)] = true;
                    }
                    else if (southdoor.ContainsKey(new Vector2(x, y - 1)))
                    {
                        corridor.Add(new Vector2((x - 1) * 256, y * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                        list.Add(new Vector2((x - 1) * 256, (y + 1) * 256));
                        corridor = new List<Vector2>();
                        corridorturn.Add(list);
                        southdoor[new Vector2(x, y - 1)] = true;
                        visitedcorridors[new Vector2(x - 1, y)] = true;
                    }
                    else
                    {
                        corridor.Add(new Vector2(x * 256, y * 256));
                        corridorturn.Add(corridor);
                        list.Add(new Vector2(corridor[0].X, corridor[0].Y + 256));
                        list.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));

                        corridorturn.Add(list);
                        closer.Add(new Vector2(corridor[1].X, corridor[1].Y));
                        closer.Add(new Vector2(corridor[1].X, corridor[1].Y + 256));
                        corridorturn.Add(closer);
                        corridor = new List<Vector2>();
                        closer = new List<Vector2>();
                        list = new List<Vector2>();
                        visitedcorridors[new Vector2(x - 1, y)] = true;
                    }

                }

            }
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {

            DDClass.Rootobject ddobj = JsonConvert.DeserializeObject<Rootobject>(File.ReadAllText(Application.StartupPath + @"/Resources/template.dungeondraft_map"));

            ddobj.world.width = json.settings.n_cols;
            ddobj.world.height = json.settings.n_rows;
            int camerax = json.settings.n_cols / 2 * 256;
            int cameray = json.settings.n_rows / 2 * 256;

            ddobj.header.editor_state.camera_position = "Vector2( " + camerax + "," + cameray + ")";

            ddobj.header.editor_state.camera_zoom = 8;
            int splt = json.settings.n_cols * json.settings.n_rows * 16;

            string splat = "";
            for (int i = 0; i < splt; i++)
            {
                splat += "255, 0, 0, 0,";
            }
            splat = splat.Remove(splat.Length - 1, 1);
            ddobj.world.levels["0"].terrain.splat = "PoolByteArray(" + splat + ")";
            string cells = "";
            List<string> colors = new List<string>();

            for (int i = 0; i < json.settings.n_cols * json.settings.n_rows; i++)
            {
                cells += "-1,";
                colors.Add("ffffffff");
            }
            cells = cells.Remove(cells.Length - 1, 1);

            ddobj.world.levels["0"].tiles.cells = "PoolIntArray(" + cells + ")";
            ddobj.world.levels["0"].tiles.colors = colors.ToArray();
            string cave = "";
            int cavehash = (int)Math.Floor((json.settings.n_cols * json.settings.n_rows * 2) + 1.5 * (json.settings.n_cols + json.settings.n_rows) + 2);
            for (int i = 0; i < cavehash; i++)
            {
                cave += "0,";
            }
            cave = cave.Remove(cave.Length - 1, 1);
            ddobj.world.levels["0"].cave.bitmap = "PoolByteArray(" + cave + ")";
            ddobj.world.levels["0"].cave.entrance_bitmap = "PoolByteArray(" + cave + ")";

            string template = File.ReadAllText(Application.StartupPath + @"/Resources/template.dungeondraft_map");
            string xtopleft, ytopleft, xtopright, ytopright, xbotright, ybotright, xbotleft, ybotleft;

            

            doors.Add("east", new List<Vector2>());
            doors.Add("south", new List<Vector2>());
            doors.Add("west", new List<Vector2>());
            doors.Add("north", new List<Vector2>());
            for (int i = 1; i < json.rooms.Count; i++)
            {
                if (json.rooms[i].doors.east != null)
                {
                    for (int j = 0; j < json.rooms[i].doors.east.Count; j++)
                    {
                        doors["east"].Add(new Vector2(json.rooms[i].doors.east[j].col, json.rooms[i].doors.east[j].row));
                    }
                }
                if (json.rooms[i].doors.south != null)
                {
                    for (int j = 0; j < json.rooms[i].doors.south.Count; j++)
                    {
                        doors["south"].Add(new Vector2(json.rooms[i].doors.south[j].col, json.rooms[i].doors.south[j].row));
                    }
                }
                if (json.rooms[i].doors.west != null)
                {
                    for (int j = 0; j < json.rooms[i].doors.west.Count; j++)
                    {
                        doors["west"].Add(new Vector2(json.rooms[i].doors.west[j].col, json.rooms[i].doors.west[j].row));
                    }
                }
                if (json.rooms[i].doors.north != null)
                {
                    for (int j = 0; j < json.rooms[i].doors.north.Count; j++)
                    {
                        doors["north"].Add(new Vector2(json.rooms[i].doors.north[j].col, json.rooms[i].doors.north[j].row));
                    }
                }

            }


            for (int i = 0; i < json.cells.Count; i++)
            {
                for (int j = 0; j < json.cells[i].Count; j++)
                {
                    if (json.cells[i][j] == 4 || json.cells[i][j] > 1600000000 || json.cells[i][j] >= 130000 || json.cells[i][j] == 65540 || json.cells[i][j] == 2097156 || json.cells[i][j] == 524292)
                    {
                        //theese are to be considered corridor tiles
                        if (json.cells[i][j] == 131076)
                        {
                            visitedcorridors.Add(new Vector2(j, i), false);
                            //doorlist.Add(new Vector2(j, i));
                            //connectedoors.Add(new Vector2(j, i), false);
                        }
                        else
                        {
                            visitedcorridors.Add(new Vector2(j, i), false);
                        }

                    }

                }
            }


            for (int i = 0; i < doors["east"].Count; i++)
            {
                try
                {
                    visitedcorridors.Add(doors["east"][i], false);
                }
                catch (Exception)
                {


                }


                doorlist.Add(doors["east"][i]);
                eastdoor.Add(doors["east"][i], false);
            }
            for (int i = 0; i < doors["south"].Count; i++)
            {
                try
                {
                    visitedcorridors.Add(doors["south"][i], false);
                }
                catch (Exception)
                {


                }

                doorlist.Add(doors["south"][i]);
                southdoor.Add(doors["south"][i], false);
            }
            for (int i = 0; i < doors["west"].Count; i++)
            {
                try
                {
                    visitedcorridors.Add(new Vector2(doors["west"][i].X, doors["west"][i].Y), false);
                }
                catch (Exception)
                {


                }

                doorlist.Add(new Vector2(doors["west"][i].X + 1, doors["west"][i].Y));
                westdoor.Add(new Vector2(doors["west"][i].X + 1, doors["west"][i].Y), false);
            }
            for (int i = 0; i < doors["north"].Count; i++)
            {
                try
                {
                    visitedcorridors.Add(new Vector2(doors["north"][i].X, doors["north"][i].Y), false);
                }
                catch (Exception)
                {


                }

                doorlist.Add(new Vector2(doors["north"][i].X, doors["north"][i].Y + 1));
                northdoor.Add(new Vector2(doors["north"][i].X, doors["north"][i].Y + 1), false);
            }

            /*
            for (int i = 1; i < json.rooms.Count; i++)
            {
                if (json.rooms[i].doors.east != null)
                {

                    for (int j = 0; j < json.rooms[i].doors.east.Count; j++)
                    {
                        try
                        {

                            visitedcorridors.Add(new Vector2(json.rooms[i].doors.east[j].col, json.rooms[i].doors.east[j].row), true);
                            doorlist.Add(new Vector2(json.rooms[i].doors.east[j].col, json.rooms[i].doors.east[j].row));
                            connectedoors.Add(new Vector2(json.rooms[i].doors.east[j].col, json.rooms[i].doors.east[j].row), false);
                        }
                        catch (Exception)
                        {

                        }

                    }
                }
                if (json.rooms[i].doors.south != null)
                {
                    for (int j = 0; j < json.rooms[i].doors.south.Count; j++)
                    {
                        try
                        {
                            visitedcorridors.Add(new Vector2(json.rooms[i].doors.south[j].col, json.rooms[i].doors.south[j].row), true);
                            doorlist.Add(new Vector2(json.rooms[i].doors.south[j].col, json.rooms[i].doors.south[j].row));
                            connectedoors.Add(new Vector2(json.rooms[i].doors.south[j].col, json.rooms[i].doors.south[j].row), false);
                        }
                        catch (Exception)
                        {

                        }

                    }
                }
                if (json.rooms[i].doors.west != null)
                {
                    for (int j = 0; j < json.rooms[i].doors.west.Count; j++)
                    {
                        try
                        {
                            if (connectedoors.ContainsKey(new Vector2(json.rooms[i].doors.west[j].col, json.rooms[i].doors.west[j].row)))
                            {
                                connectedoors.Remove(new Vector2(json.rooms[i].doors.west[j].col, json.rooms[i].doors.west[j].row));
                                doorlist.Remove(new Vector2(json.rooms[i].doors.west[j].col, json.rooms[i].doors.west[j].row));
                            }
                            visitedcorridors.Add(new Vector2(json.rooms[i].doors.west[j].col+1, json.rooms[i].doors.west[j].row), true);
                            doorlist.Add(new Vector2(json.rooms[i].doors.west[j].col+1, json.rooms[i].doors.west[j].row));
                            connectedoors.Add(new Vector2(json.rooms[i].doors.west[j].col + 1, json.rooms[i].doors.west[j].row), false);
                        }
                        catch (Exception)
                        {

                        }

                    }
                }
                if (json.rooms[i].doors.north != null)
                {
                    for (int j = 0; j < json.rooms[i].doors.north.Count; j++)
                    {
                        try
                        {
                            if (connectedoors.ContainsKey(new Vector2(json.rooms[i].doors.north[j].col, json.rooms[i].doors.north[j].row)))
                            {
                                connectedoors.Remove(new Vector2(json.rooms[i].doors.north[j].col, json.rooms[i].doors.north[j].row));
                                doorlist.Remove(new Vector2(json.rooms[i].doors.north[j].col, json.rooms[i].doors.north[j].row));
                            }
                            visitedcorridors.Add(new Vector2(json.rooms[i].doors.north[j].col, json.rooms[i].doors.north[j].row+1), true);
                            doorlist.Add(new Vector2(json.rooms[i].doors.north[j].col, json.rooms[i].doors.north[j].row + 1));
                            connectedoors.Add(new Vector2(json.rooms[i].doors.north[j].col, json.rooms[i].doors.north[j].row + 1), false);
                        }
                        catch (Exception)
                        {

                        }

                    }
                }

            }
            */

            List<Pattern> patlist = new List<Pattern>();
            List<Wall> walllist = new List<Wall>();
            List<Wall> corridorlist = new List<Wall>();

            corridor = new List<Vector2>();
            List<Portal> portlist = new List<Portal>();
            int id = 5;
            List<Vector2> patterns = new List<Vector2>();
            for (int i = 0; i < json.cells.Count; i++)
            {

                for (int j = 0; j < json.cells[i].Count; j++)
                {
                    if (json.cells[i][j] == 4 || json.cells[i][j] > 1600000000 || json.cells[i][j] >= 130000 || json.cells[i][j] == 65540 || json.cells[i][j] == 2097156 || json.cells[i][j] == 524292)
                    {
                        if (patterns.Count == 0)
                        {
                            patterns.Add(new Vector2(j * 256, i * 256));
                        }
                    }
                    else
                    {
                        if (patterns.Count == 1)
                        {
                            patterns.Add(new Vector2(j * 256, i * 256));
                            Pattern pat = new Pattern();
                            pat.position = "Vector2( 0, 0 )";
                            pat.shape_rotation = 0;
                            pat.scale = "Vector2( 1, 1 )";
                            pat.points = "PoolVector2Array(" + patterns[0].X + "," + patterns[0].Y + "," + patterns[1].X + "," + patterns[0].Y + "," + patterns[1].X + "," + (patterns[1].Y + 256) + "," + patterns[0].X + "," + (patterns[1].Y + 256) + ")";
                            pat.layer = 100;
                            pat.color = "ff7f7e71";
                            pat.outline = false;
                            pat.texture = "res://textures/tilesets/simple/tileset_cave.png";
                            pat.rotation = 0;
                            pat.node_id = id.ToString();

                            patlist.Add(pat);
                            patterns = new List<Vector2>();
                            id++;
                        }
                    }

                }
            }



            for (int i = 1; i < json.rooms.Count; i++)
            {
                corridorturn = new List<List<Vector2>>();

                xtopleft = (json.rooms[i].west * 256).ToString();
                ytopleft = (json.rooms[i].north * 256).ToString();
                xtopright = ((json.rooms[i].east + 1) * 256).ToString();
                ytopright = (json.rooms[i].north * 256).ToString();
                xbotleft = (json.rooms[i].west * 256).ToString();
                ybotleft = ((json.rooms[i].south + 1) * 256).ToString();
                xbotright = ((json.rooms[i].east + 1) * 256).ToString();
                ybotright = ((json.rooms[i].south + 1) * 256).ToString();

                Pattern pat = new Pattern();
                Wall wall = new Wall();
                portlist = new List<Portal>();

                pat.position = "Vector2( 0, 0 )";
                pat.shape_rotation = 0;
                pat.scale = "Vector2( 1, 1 )";
                pat.points = "PoolVector2Array(" + xtopleft + "," + ytopleft + "," + xtopright + "," + ytopright + "," + xbotright + "," + ybotright + "," + xbotleft + "," + ybotleft + ")";
                pat.layer = 100;
                pat.color = "ff7f7e71";
                pat.outline = false;
                pat.texture = "res://textures/tilesets/simple/tileset_cave.png";
                pat.rotation = 0;
                pat.node_id = id.ToString();

                patlist.Add(pat);


                id++;
                int wallid = id;
                int xpos = -1;
                int ypos = -1;
                if (json.rooms[i].doors.east != null)
                {
                    for (int j = 0; j < json.rooms[i].doors.east.Count; j++)
                    {
                        id++;
                        Portal portal = new Portal();

                        //starting point
                        int doorx = json.rooms[i].doors.east[j].col;
                        int doory = json.rooms[i].doors.east[j].row;
                        recursions = 0;
                        corridor = new List<Vector2>();
                        corridor.Add(new Vector2(doorx * 256, doory * 256));



                        Pathfinding(doorx, doory, "", json.cells, "right");
                        eastdoor[new Vector2(doorx, doory)] = true;
                        xpos = (json.rooms[i].doors.east[j].col) * 256;
                        ypos = (int)Math.Floor((json.rooms[i].doors.east[j].row + 0.5f) * 256);
                        portal.position = "Vector2(" + xpos + "," + ypos + ")";
                        portal.rotation = 1.570796f;
                        portal.scale = "Vector2(1,1)";
                        portal.direction = "Vector2(0,1)";
                        portal.texture = "res://textures/portals/door_00.png";
                        portal.radius = 128;
                        portal.wall_id = wallid.ToString();
                        portal.point_index = 1;


                        float walldistance = 1.0f + ((json.rooms[i].doors.east[j].row - json.rooms[i].north + 0.5f) / (json.rooms[i].south - json.rooms[i].north + 1));

                        portal.wall_distance = walldistance;
                        portal.closed = true;
                        portal.node_id = id.ToString();
                        portlist.Add(portal);
                    }
                }
                if (json.rooms[i].doors.south != null)
                {
                    for (int j = 0; j < json.rooms[i].doors.south.Count; j++)
                    {
                        id++;
                        Portal portal = new Portal();

                        //starting point
                        int doorx = json.rooms[i].doors.south[j].col;
                        int doory = json.rooms[i].doors.south[j].row;
                        recursions = 0;
                        corridor = new List<Vector2>();

                        corridor.Add(new Vector2(doorx * 256, doory * 256));

                        Pathfinding(doorx, doory, "", json.cells, "down");
                        southdoor[new Vector2(doorx, doory)] = true;
                        xpos = (int)Math.Floor((json.rooms[i].doors.south[j].col + 0.5f) * 256);
                        ypos = (json.rooms[i].doors.south[j].row) * 256;
                        portal.position = "Vector2(" + xpos + "," + ypos + ")";
                        portal.rotation = 0;
                        portal.scale = "Vector2(1,1)";
                        portal.direction = "Vector2(-1,0)";
                        portal.texture = "res://textures/portals/door_00.png";
                        portal.radius = 128;
                        portal.wall_id = wallid.ToString();
                        portal.point_index = 2;
                        float walldistance = 2.0f + ((json.rooms[i].east - json.rooms[i].doors.south[j].col + 0.5f) / (json.rooms[i].east - json.rooms[i].west + 1));

                        portal.wall_distance = walldistance;
                        portal.closed = true;
                        portal.node_id = id.ToString();
                        portlist.Add(portal);
                    }
                }
                if (json.rooms[i].doors.west != null)
                {
                    for (int j = 0; j < json.rooms[i].doors.west.Count; j++)
                    {
                        id++;
                        Portal portal = new Portal();

                        //starting point
                        int doorx = json.rooms[i].doors.west[j].col + 1;
                        int doory = json.rooms[i].doors.west[j].row;
                        recursions = 0;
                        corridor = new List<Vector2>();
                        corridor.Add(new Vector2(doorx * 256, doory * 256));

                        Pathfinding(doorx, doory, "", json.cells, "left");
                        westdoor[new Vector2(doorx, doory)] = true;
                        xpos = (json.rooms[i].doors.west[j].col + 1) * 256;
                        ypos = (int)Math.Floor((json.rooms[i].doors.west[j].row + 0.5f) * 256);
                        portal.position = "Vector2(" + xpos + "," + ypos + ")";
                        portal.rotation = -1.570796f;
                        portal.scale = "Vector2(1,1)";
                        portal.direction = "Vector2(0,-1)";
                        portal.texture = "res://textures/portals/door_00.png";
                        portal.radius = 128;
                        portal.wall_id = wallid.ToString();
                        portal.point_index = 3;

                        float walldistance = 3.0f + ((json.rooms[i].south - json.rooms[i].doors.west[j].row + 0.5f) / (json.rooms[i].south - json.rooms[i].north + 1));

                        portal.wall_distance = walldistance;
                        portal.closed = true;
                        portal.node_id = id.ToString();
                        portlist.Add(portal);
                    }
                }
                if (json.rooms[i].doors.north != null)
                {
                    for (int j = 0; j < json.rooms[i].doors.north.Count; j++)
                    {
                        id++;
                        Portal portal = new Portal();

                        //starting point
                        int doorx = json.rooms[i].doors.north[j].col;
                        int doory = json.rooms[i].doors.north[j].row + 1;

                        corridor = new List<Vector2>();
                        corridor.Add(new Vector2(doorx * 256, doory * 256));

                        Pathfinding(doorx, doory, "", json.cells, "up");
                        northdoor[new Vector2(doorx, doory)] = true;
                        xpos = (int)Math.Floor((json.rooms[i].doors.north[j].col + 0.5f) * 256);
                        ypos = (json.rooms[i].doors.north[j].row + 1) * 256;
                        portal.position = "Vector2(" + xpos + "," + ypos + ")";
                        portal.rotation = 0;
                        portal.scale = "Vector2(1,1)";
                        portal.direction = "Vector2(1,0)";
                        portal.texture = "res://textures/portals/door_00.png";
                        portal.radius = 128;
                        portal.wall_id = wallid.ToString();
                        portal.point_index = 0;
                        float walldistance = 0.0f + ((json.rooms[i].doors.north[j].col - json.rooms[i].west + 0.5f) / (json.rooms[i].east - json.rooms[i].west + 1));

                        portal.wall_distance = walldistance;
                        portal.closed = true;
                        portal.node_id = id.ToString();
                        portlist.Add(portal);
                    }
                }

                id++;


                for (int k = 0; k < corridorturn.Count; k++)
                {
                    Wall wallbuffer = new Wall();
                    wallbuffer.points = "PoolVector2Array( " + corridorturn[k][0].X + "," + corridorturn[k][0].Y + "," + corridorturn[k][1].X + "," + corridorturn[k][1].Y + " )";
                    wallbuffer.texture = "res://textures/walls/stone.png";
                    wallbuffer.color = "ff605f58";
                    wallbuffer.loop = false;
                    wallbuffer.type = 1;
                    wallbuffer.joint = 0;
                    wallbuffer.normalize_uv = true;
                    wallbuffer.shadow = false;
                    wallbuffer.node_id = id.ToString();
                    wallbuffer.portals = new List<Portal>().ToArray();
                    walllist.Add(wallbuffer);
                    id++;

                    /*
                    pat = new Pattern();
                    pat.position = "Vector2( 0, 0 )";
                    pat.shape_rotation = 0;
                    pat.scale = "Vector2( 1, 1 )";
                    pat.points = "PoolVector2Array("+ corridorturn[k][0].X + "," + corridorturn[k][0].Y + "," + corridorturn[k][1].X + "," + corridorturn[k][1].Y + "," + corridorturn[k][1].X + "," + (corridorturn[k][1].Y+256)+"," +corridorturn[k][0].X + "," + (corridorturn[k][1].Y + 256) + ")";
                    pat.layer = 100;
                    pat.color = "ff7f7e71";
                    pat.outline = false;
                    pat.texture = "res://textures/tilesets/simple/tileset_cave.png";
                    pat.rotation = 0;
                    pat.node_id = id.ToString();

                    patlist.Add(pat);
                    id++;
                    */
                }






                /*
                if (corridorturn.Count>0) 
                {
                    wallbuffer.points += " )";
                    wallbuffer.texture = "res://textures/walls/stone.png";
                    wallbuffer.color = "ff605f58";
                    wallbuffer.loop = false;
                    wallbuffer.type = 1;
                    wallbuffer.joint = 0;
                    wallbuffer.normalize_uv = true;
                    wallbuffer.shadow = false;
                    wallbuffer.node_id = id.ToString();
                    wallbuffer.portals = new List<Portal>().ToArray();
                    walllist.Add(wallbuffer);
                }
                */

                wall.points = "PoolVector2Array( " + xtopleft + "," + ytopleft + "," + xtopright + "," + ytopright + "," + xbotright + "," + ybotright + "," + xbotleft + "," + ybotleft + " )";
                wall.texture = "res://textures/walls/stone.png";
                wall.color = "ff605f58";
                wall.loop = true;
                wall.type = 1;
                wall.joint = 0;
                wall.normalize_uv = true;
                wall.shadow = false;
                wall.node_id = wallid.ToString();
                wall.portals = portlist.ToArray();
                walllist.Add(wall);

                id++;
            }



            ddobj.world.levels["0"].patterns = patlist.ToArray();
            ddobj.world.levels["0"].walls = walllist.ToArray();

            string output = JsonConvert.SerializeObject(ddobj, Formatting.Indented);
            File.WriteAllText(Application.StartupPath + @"/OUTPUT/donjonmap.dungeondraft_map", output);

            MessageBox.Show("Done");
        }
    }



    public class A
    {
        public string detail { get; set; }
        public string key { get; set; }
        public List<Mark> marks { get; set; }
        public string summary { get; set; }
    }

    public class C
    {
        public string detail { get; set; }
        public string key { get; set; }
        public List<Mark> marks { get; set; }
        public string summary { get; set; }
    }

    public class CellBit
    {
        public int aperture { get; set; }
        public int arch { get; set; }
        public int block { get; set; }
        public int corridor { get; set; }
        public int door { get; set; }
        public long label { get; set; }
        public int locked { get; set; }
        public int nothing { get; set; }
        public int perimeter { get; set; }
        public int portcullis { get; set; }
        public int room { get; set; }
        public int room_id { get; set; }
        public int secret { get; set; }
        public int stair_down { get; set; }
        public int stair_up { get; set; }
        public int trapped { get; set; }
    }

    public class Contents
    {
        public Detail detail { get; set; }
        public string inhabited { get; set; }
        public string summary { get; set; }
    }

    public class CorridorFeatures
    {
        public A a { get; set; }
        public C c { get; set; }
    }

    public class Detail
    {
        public List<string> monster { get; set; }
        public string room_features { get; set; }
        public List<string> trap { get; set; }
        public List<string> hidden_treasure { get; set; }
    }

    public class Details
    {
        public string floor { get; set; }
        public string illumination { get; set; }
        public string size { get; set; }
        public object special { get; set; }
        public string temperature { get; set; }
        public string walls { get; set; }
    }

    public class Doors
    {
        public List<North> north { get; set; }
        public List<South> south { get; set; }
        public List<East> east { get; set; }
        public List<West> west { get; set; }
    }

    public class East
    {
        public int col { get; set; }
        public string desc { get; set; }
        public int out_id { get; set; }
        public int row { get; set; }
        public string type { get; set; }
        public string trap { get; set; }
    }

    public class Egress
    {
        public int col { get; set; }
        public int depth { get; set; }
        public string dir { get; set; }
        public int room_id { get; set; }
        public int row { get; set; }
        public string type { get; set; }
    }

    public class Mark
    {
        public int col { get; set; }
        public int row { get; set; }
    }

    public class North
    {
        public int col { get; set; }
        public string desc { get; set; }
        public int row { get; set; }
        public string type { get; set; }
        public int? out_id { get; set; }
        public string trap { get; set; }
    }

    public class Room
    {
        public int area { get; set; }
        public int col { get; set; }
        public Contents contents { get; set; }
        public Doors doors { get; set; }
        public int east { get; set; }
        public int height { get; set; }
        public int id { get; set; }
        public int north { get; set; }
        public int row { get; set; }
        public string shape { get; set; }
        public string size { get; set; }
        public int south { get; set; }
        public int west { get; set; }
        public int width { get; set; }
    }

    public class Root
    {
        public CellBit cell_bit { get; set; }
        public List<List<int>> cells { get; set; }
        public CorridorFeatures corridor_features { get; set; }
        public Details details { get; set; }
        public List<Egress> egress { get; set; }
        public List<Room> rooms { get; set; }
        public Settings settings { get; set; }
        public List<Stair> stairs { get; set; }
        public WanderingMonsters wandering_monsters { get; set; }
    }

    public class Settings
    {
        public string add_stairs { get; set; }
        public int bleed { get; set; }
        public int cell_size { get; set; }
        public string corridor_layout { get; set; }
        public string door_set { get; set; }
        public string dungeon_layout { get; set; }
        public string dungeon_size { get; set; }
        public string grid { get; set; }
        public string image_size { get; set; }
        public string infest { get; set; }
        public int last_room_id { get; set; }
        public int level { get; set; }
        public string map_cols { get; set; }
        public string map_rows { get; set; }
        public string map_style { get; set; }
        public int max_col { get; set; }
        public int max_row { get; set; }
        public string motif { get; set; }
        public int n_cols { get; set; }
        public int n_i { get; set; }
        public int n_j { get; set; }
        public int n_pc { get; set; }
        public int n_rooms { get; set; }
        public int n_rows { get; set; }
        public string name { get; set; }
        public string peripheral_egress { get; set; }
        public string remove_arcs { get; set; }
        public string remove_deadends { get; set; }
        public string room_layout { get; set; }
        public string room_polymorph { get; set; }
        public string room_size { get; set; }
        public int seed { get; set; }
    }

    public class South
    {
        public int col { get; set; }
        public string desc { get; set; }
        public int out_id { get; set; }
        public int row { get; set; }
        public string type { get; set; }
        public string trap { get; set; }
    }

    public class Stair
    {
        public int col { get; set; }
        public string dir { get; set; }
        public string key { get; set; }
        public int row { get; set; }
    }

    public class WanderingMonsters
    {
        public string _1 { get; set; }

        public string _2 { get; set; }
        public string _3 { get; set; }

        public string _4 { get; set; }

        public string _5 { get; set; }

        public string _6 { get; set; }
    }

    public class West
    {
        public int col { get; set; }
        public string desc { get; set; }
        public int out_id { get; set; }
        public int row { get; set; }
        public string type { get; set; }
        public string trap { get; set; }

    }





}