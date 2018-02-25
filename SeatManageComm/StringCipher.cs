using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.SeatManageComm
{
    public class StringCipherCls
    {
        ///************************************************/
        ///此模块包含三个关于加密的函数                    /
        ///                                                /
        ///GetRandomKey()用于获得一个随机生成的密钥        /
        ///StringCipher()用于加密                          /
        ///StringDecipher()用于解密                        /
        ///此处采用的加密算法为DES(CBC)算法                /
        ///Last Modify By                                  /
        ///Lotus.King 2001/01/08                           /
        ///请修改下面的常数cnLeastLength,以决定密文的规模  /
        ///************************************************/
        byte[] IP = new byte[65]; //初始置换IP
        byte[] IPC = new byte[65]; //初始逆置换IP-1
        byte[] E = new byte[49]; //扩展函数E
        byte[] P = new byte[33]; //置换P
        byte[] PC1 = new byte[57]; //置换PC-1
        byte[] PC2 = new byte[57]; //置换PC-2
        byte[, ,] s = new byte[9, 5, 17]; //8个S盒每个为(4*8)
        Bit[,] KI = new Bit[17, 49]; //16个密钥K的置换选择，每个48位
        private enum Bit
        {
            b0 = 0,
            b1 = 1
        }
        /// <summary>
        /// 
        /// </summary>
        public string Key = "79 98 8 49 A1 B5 F4 1C";
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //CHANGE HERE IF YOU WANT TO CHANGE THE SIZE OF ENCROPTOGRAPH
        const short cnLeastLength = 128; //密文应有多少个字节(64的倍数)
        /// <summary>
        /// 初始化各置换矩阵
        /// </summary>
        private void InitParam()
        {
            IP[0] = 58;
            IP[1] = 50;
            IP[2] = 42;
            IP[3] = 34;
            IP[4] = 26;
            IP[5] = 18;
            IP[6] = 10;
            IP[7] = 2;
            IP[8] = 60;

            IP[9] = 52;
            IP[10] = 44;
            IP[11] = 36;
            IP[12] = 28;
            IP[13] = 20;
            IP[14] = 12;
            IP[15] = 4;
            IP[16] = 62;

            IP[17] = 54;
            IP[18] = 46;
            IP[19] = 38;
            IP[20] = 30;
            IP[21] = 22;
            IP[22] = 14;
            IP[23] = 6;
            IP[24] = 64;

            IP[25] = 56;
            IP[26] = 48;
            IP[27] = 40;
            IP[28] = 32;
            IP[29] = 24;
            IP[30] = 16;
            IP[31] = 8;
            IP[32] = 57;

            IP[33] = 49;
            IP[34] = 41;
            IP[35] = 33;
            IP[36] = 25;
            IP[37] = 17;
            IP[38] = 9;
            IP[39] = 1;
            IP[40] = 59;

            IP[41] = 51;
            IP[42] = 43;
            IP[43] = 35;
            IP[44] = 27;
            IP[45] = 19;
            IP[46] = 11;
            IP[47] = 3;
            IP[48] = 61;

            IP[49] = 53;
            IP[50] = 45;
            IP[51] = 37;
            IP[52] = 29;
            IP[53] = 21;
            IP[54] = 13;
            IP[55] = 5;
            IP[56] = 63;

            IP[57] = 55;
            IP[58] = 47;
            IP[59] = 39;
            IP[60] = 31;
            IP[61] = 23;
            IP[62] = 15;
            IP[63] = 7;
            IPC[0] = 40;

            IPC[1] = 8;
            IPC[2] = 48;
            IPC[3] = 16;
            IPC[4] = 56;
            IPC[5] = 24;
            IPC[6] = 64;
            IPC[7] = 32;
            IPC[8] = 39;

            IPC[9] = 7;
            IPC[10] = 47;
            IPC[11] = 15;
            IPC[12] = 55;
            IPC[13] = 23;
            IPC[14] = 63;
            IPC[15] = 31;
            IPC[16] = 38;

            IPC[17] = 6;
            IPC[18] = 46;
            IPC[19] = 14;
            IPC[20] = 54;
            IPC[21] = 22;
            IPC[22] = 62;
            IPC[23] = 30;
            IPC[24] = 37;

            IPC[25] = 5;
            IPC[26] = 45;
            IPC[27] = 13;
            IPC[28] = 53;
            IPC[29] = 21;
            IPC[30] = 61;
            IPC[31] = 29;
            IPC[32] = 36;

            IPC[33] = 4;
            IPC[34] = 44;
            IPC[35] = 12;
            IPC[36] = 52;
            IPC[37] = 20;
            IPC[38] = 60;
            IPC[39] = 28;
            IPC[40] = 35;

            IPC[41] = 3;
            IPC[42] = 43;
            IPC[43] = 11;
            IPC[44] = 51;
            IPC[45] = 19;
            IPC[46] = 59;
            IPC[47] = 27;
            IPC[48] = 34;

            IPC[49] = 2;
            IPC[50] = 42;
            IPC[51] = 10;
            IPC[52] = 50;
            IPC[53] = 18;
            IPC[54] = 58;
            IPC[55] = 26;
            IPC[56] = 33;

            IPC[57] = 1;
            IPC[58] = 41;
            IPC[59] = 9;
            IPC[60] = 49;
            IPC[61] = 17;
            IPC[62] = 57;
            IPC[63] = 25;
            E[0] = 32;
            E[1] = 1;
            E[2] = 2;
            E[3] = 3;
            E[4] = 4;
            E[5] = 5;
            E[6] = 4;
            E[7] = 5;
            E[8] = 6;
            E[9] = 7;
            E[10] = 8;
            E[11] = 9;
            E[12] = 8;
            E[13] = 9;
            E[14] = 10;
            E[15] = 11;
            E[16] = 12;
            E[17] = 13;
            E[18] = 12;
            E[19] = 13;
            E[20] = 14;
            E[21] = 15;
            E[22] = 16;
            E[23] = 17;
            E[24] = 16;
            E[25] = 17;
            E[26] = 18;
            E[27] = 19;
            E[28] = 20;
            E[29] = 21;
            E[30] = 20;
            E[31] = 21;
            E[32] = 22;
            E[33] = 23;
            E[34] = 24;
            E[35] = 25;
            E[36] = 24;
            E[37] = 25;
            E[38] = 26;
            E[39] = 27;
            E[40] = 28;
            E[41] = 29;
            E[42] = 28;
            E[43] = 29;
            E[44] = 30;
            E[45] = 31;
            E[46] = 32;
            E[47] = 1;

            P[0] = 16;
            P[1] = 7;
            P[2] = 20;
            P[3] = 21;
            P[4] = 29;
            P[5] = 12;
            P[6] = 28;
            P[7] = 17;
            P[8] = 1;
            P[9] = 15;
            P[10] = 23;
            P[11] = 26;
            P[12] = 5;
            P[13] = 18;
            P[14] = 31;
            P[15] = 10;
            P[16] = 2;
            P[17] = 8;
            P[18] = 24;
            P[19] = 14;
            P[20] = 32;
            P[21] = 27;
            P[22] = 3;
            P[23] = 9;
            P[24] = 19;
            P[25] = 13;
            P[26] = 30;
            P[27] = 6;
            P[28] = 22;
            P[29] = 11;
            P[30] = 4;
            P[31] = 25;
            PC1[0] = 57;

            PC1[1] = 49;
            PC1[2] = 41;
            PC1[3] = 33;
            PC1[4] = 25;
            PC1[5] = 17;
            PC1[6] = 9;
            PC1[7] = 1;

            PC1[8] = 58;
            PC1[9] = 50;
            PC1[10] = 42;
            PC1[11] = 34;
            PC1[12] = 26;
            PC1[13] = 18;
            PC1[14] = 10;

            PC1[15] = 2;
            PC1[16] = 59;
            PC1[17] = 51;
            PC1[18] = 43;
            PC1[19] = 35;
            PC1[20] = 27;
            PC1[21] = 19;

            PC1[22] = 11;
            PC1[23] = 3;
            PC1[24] = 60;
            PC1[25] = 52;
            PC1[26] = 44;
            PC1[27] = 36;
            PC1[28] = 63;

            PC1[29] = 55;
            PC1[30] = 47;
            PC1[31] = 39;
            PC1[32] = 31;
            PC1[33] = 23;
            PC1[34] = 15;
            PC1[35] = 7;

            PC1[36] = 62;
            PC1[37] = 54;
            PC1[38] = 46;
            PC1[39] = 38;
            PC1[40] = 30;
            PC1[41] = 22;
            PC1[42] = 14;

            PC1[43] = 6;
            PC1[44] = 61;
            PC1[45] = 53;
            PC1[46] = 45;
            PC1[47] = 37;
            PC1[48] = 29;
            PC1[49] = 21;

            PC1[50] = 13;
            PC1[51] = 5;
            PC1[52] = 28;
            PC1[53] = 20;
            PC1[54] = 12;
            PC1[55] = 4;

            PC2[0] = 14;

            PC2[1] = 17;
            PC2[2] = 11;
            PC2[3] = 24;
            PC2[4] = 1;
            PC2[5] = 5;
            PC2[6] = 3;

            PC2[7] = 28;
            PC2[8] = 15;
            PC2[9] = 6;
            PC2[10] = 21;
            PC2[11] = 10;
            PC2[12] = 23;

            PC2[13] = 19;
            PC2[14] = 12;
            PC2[15] = 4;
            PC2[16] = 26;
            PC2[17] = 8;
            PC2[18] = 16;

            PC2[19] = 7;
            PC2[20] = 27;
            PC2[21] = 20;
            PC2[22] = 13;
            PC2[23] = 2;
            PC2[24] = 41;

            PC2[25] = 52;
            PC2[26] = 31;
            PC2[27] = 37;
            PC2[28] = 47;
            PC2[29] = 55;
            PC2[30] = 30;

            PC2[31] = 40;
            PC2[32] = 51;
            PC2[33] = 45;
            PC2[34] = 33;
            PC2[35] = 48;
            PC2[36] = 44;

            PC2[37] = 49;
            PC2[38] = 39;
            PC2[39] = 56;
            PC2[40] = 34;
            PC2[41] = 53;
            PC2[42] = 46;

            PC2[43] = 42;
            PC2[44] = 30;
            PC2[45] = 36;
            PC2[46] = 29;
            PC2[47] = 32;
            s[1, 0, 0] = 14;

            s[1, 0, 1] = 4;
            s[1, 0, 2] = 13;
            s[1, 0, 3] = 1;
            s[1, 0, 4] = 2;
            s[1, 0, 5] = 15;
            s[1, 0, 6] = 11;
            s[1, 0, 7] = 8;
            s[1, 0, 8] = 3;

            s[1, 0, 9] = 10;
            s[1, 0, 10] = 6;
            s[1, 0, 11] = 12;
            s[1, 0, 12] = 5;
            s[1, 0, 13] = 9;
            s[1, 0, 14] = 0;
            s[1, 0, 15] = 7;
            s[1, 1, 0] = 0;

            s[1, 1, 1] = 15;
            s[1, 1, 2] = 7;
            s[1, 1, 3] = 4;
            s[1, 1, 4] = 14;
            s[1, 1, 5] = 2;
            s[1, 1, 6] = 13;
            s[1, 1, 7] = 1;
            s[1, 1, 8] = 10;

            s[1, 1, 9] = 6;
            s[1, 1, 10] = 12;
            s[1, 1, 11] = 11;
            s[1, 1, 12] = 9;
            s[1, 1, 13] = 5;
            s[1, 1, 14] = 3;
            s[1, 1, 15] = 8;
            s[1, 2, 0] = 4;

            s[1, 2, 1] = 1;
            s[1, 2, 2] = 14;
            s[1, 2, 3] = 8;
            s[1, 2, 4] = 13;
            s[1, 2, 5] = 6;
            s[1, 2, 6] = 2;
            s[1, 2, 7] = 11;
            s[1, 2, 8] = 15;

            s[1, 2, 9] = 12;
            s[1, 2, 10] = 9;
            s[1, 2, 11] = 7;
            s[1, 2, 12] = 3;
            s[1, 2, 13] = 10;
            s[1, 2, 14] = 5;
            s[1, 2, 15] = 0;
            s[1, 3, 0] = 15;

            s[1, 3, 1] = 12;
            s[1, 3, 2] = 8;
            s[1, 3, 3] = 2;
            s[1, 3, 4] = 4;
            s[1, 3, 5] = 9;
            s[1, 3, 6] = 1;
            s[1, 3, 7] = 7;
            s[1, 3, 8] = 5;

            s[1, 3, 9] = 11;
            s[1, 3, 10] = 3;
            s[1, 3, 11] = 14;
            s[1, 3, 12] = 10;
            s[1, 3, 13] = 0;
            s[1, 3, 14] = 6;
            s[1, 3, 15] = 13;

            s[2, 0, 0] = 15;

            s[2, 0, 1] = 1;
            s[2, 0, 2] = 8;
            s[2, 0, 3] = 14;
            s[2, 0, 4] = 6;
            s[2, 0, 5] = 11;
            s[2, 0, 6] = 3;
            s[2, 0, 7] = 4;
            s[2, 0, 8] = 9;

            s[2, 0, 9] = 7;
            s[2, 0, 10] = 2;
            s[2, 0, 11] = 13;
            s[2, 0, 12] = 12;
            s[2, 0, 13] = 0;
            s[2, 0, 14] = 5;
            s[2, 0, 15] = 10;
            s[2, 1, 0] = 3;

            s[2, 1, 1] = 13;
            s[2, 1, 2] = 4;
            s[2, 1, 3] = 7;
            s[2, 1, 4] = 15;
            s[2, 1, 5] = 2;
            s[2, 1, 6] = 8;
            s[2, 1, 7] = 14;
            s[2, 1, 8] = 12;

            s[2, 1, 9] = 0;
            s[2, 1, 10] = 1;
            s[2, 1, 11] = 10;
            s[2, 1, 12] = 6;
            s[2, 1, 13] = 9;
            s[2, 1, 14] = 11;
            s[2, 1, 15] = 5;
            s[2, 2, 0] = 0;

            s[2, 2, 1] = 14;
            s[2, 2, 2] = 7;
            s[2, 2, 3] = 11;
            s[2, 2, 4] = 10;
            s[2, 2, 5] = 4;
            s[2, 2, 6] = 13;
            s[2, 2, 7] = 1;
            s[2, 2, 8] = 5;

            s[2, 2, 9] = 8;
            s[2, 2, 10] = 12;
            s[2, 2, 11] = 6;
            s[2, 2, 12] = 9;
            s[2, 2, 13] = 3;
            s[2, 2, 14] = 2;
            s[2, 2, 15] = 15;
            s[2, 3, 0] = 13;

            s[2, 3, 1] = 8;
            s[2, 3, 2] = 10;
            s[2, 3, 3] = 1;
            s[2, 3, 4] = 3;
            s[2, 3, 5] = 15;
            s[2, 3, 6] = 4;
            s[2, 3, 7] = 2;
            s[2, 3, 8] = 11;

            s[2, 3, 9] = 6;
            s[2, 3, 10] = 7;
            s[2, 3, 11] = 12;
            s[2, 3, 12] = 0;
            s[2, 3, 13] = 5;
            s[2, 3, 14] = 14;
            s[2, 3, 15] = 9;

            s[3, 0, 0] = 10;

            s[3, 0, 1] = 0;
            s[3, 0, 2] = 9;
            s[3, 0, 3] = 14;
            s[3, 0, 4] = 6;
            s[3, 0, 5] = 3;
            s[3, 0, 6] = 15;
            s[3, 0, 7] = 5;
            s[3, 0, 8] = 1;

            s[3, 0, 9] = 13;
            s[3, 0, 10] = 2;
            s[3, 0, 11] = 7;
            s[3, 0, 12] = 11;
            s[3, 0, 13] = 4;
            s[3, 0, 14] = 2;
            s[3, 0, 15] = 8;
            s[3, 1, 0] = 13;

            s[3, 1, 1] = 7;
            s[3, 1, 2] = 0;
            s[3, 1, 3] = 9;
            s[3, 1, 4] = 3;
            s[3, 1, 5] = 4;
            s[3, 1, 6] = 6;
            s[3, 1, 7] = 10;
            s[3, 1, 8] = 2;

            s[3, 1, 9] = 8;
            s[3, 1, 10] = 5;
            s[3, 1, 11] = 14;
            s[3, 1, 12] = 12;
            s[3, 1, 13] = 11;
            s[3, 1, 14] = 15;
            s[3, 1, 15] = 1;
            s[3, 2, 0] = 13;

            s[3, 2, 1] = 6;
            s[3, 2, 2] = 4;
            s[3, 2, 3] = 9;
            s[3, 2, 4] = 8;
            s[3, 2, 5] = 15;
            s[3, 2, 6] = 3;
            s[3, 2, 7] = 0;
            s[3, 2, 8] = 11;

            s[3, 2, 9] = 1;
            s[3, 2, 10] = 2;
            s[3, 2, 11] = 12;
            s[3, 2, 12] = 5;
            s[3, 2, 13] = 10;
            s[3, 2, 14] = 14;
            s[3, 2, 15] = 7;
            s[3, 3, 0] = 1;

            s[3, 3, 1] = 10;
            s[3, 3, 2] = 13;
            s[3, 3, 3] = 0;
            s[3, 3, 4] = 6;
            s[3, 3, 5] = 9;
            s[3, 3, 6] = 8;
            s[3, 3, 7] = 7;
            s[3, 3, 8] = 4;

            s[3, 3, 9] = 15;
            s[3, 3, 10] = 14;
            s[3, 3, 11] = 3;
            s[3, 3, 12] = 11;
            s[3, 3, 13] = 5;
            s[3, 3, 14] = 2;
            s[3, 3, 15] = 12;
            s[4, 0, 0] = 7;

            s[4, 0, 1] = 13;
            s[4, 0, 2] = 14;
            s[4, 0, 3] = 3;
            s[4, 0, 4] = 0;
            s[4, 0, 5] = 6;
            s[4, 0, 6] = 9;
            s[4, 0, 7] = 10;
            s[4, 0, 8] = 1;

            s[4, 0, 9] = 2;
            s[4, 0, 10] = 8;
            s[4, 0, 11] = 5;
            s[4, 0, 12] = 11;
            s[4, 0, 13] = 12;
            s[4, 0, 14] = 4;
            s[4, 0, 15] = 15;
            s[4, 1, 0] = 13;

            s[4, 1, 1] = 8;
            s[4, 1, 2] = 11;
            s[4, 1, 3] = 5;
            s[4, 1, 4] = 6;
            s[4, 1, 5] = 15;
            s[4, 1, 6] = 0;
            s[4, 1, 7] = 3;
            s[4, 1, 8] = 4;

            s[4, 1, 9] = 7;
            s[4, 1, 10] = 2;
            s[4, 1, 11] = 12;
            s[4, 1, 12] = 1;
            s[4, 1, 13] = 10;
            s[4, 1, 14] = 14;
            s[4, 1, 15] = 9;
            s[4, 2, 0] = 10;

            s[4, 2, 1] = 6;
            s[4, 2, 2] = 9;
            s[4, 2, 3] = 0;
            s[4, 2, 4] = 12;
            s[4, 2, 5] = 11;
            s[4, 2, 6] = 7;
            s[4, 2, 7] = 13;
            s[4, 2, 8] = 15;

            s[4, 2, 9] = 1;
            s[4, 2, 10] = 3;
            s[4, 2, 11] = 14;
            s[4, 2, 12] = 5;
            s[4, 2, 13] = 2;
            s[4, 2, 14] = 8;
            s[4, 2, 15] = 4;
            s[4, 3, 0] = 3;

            s[4, 3, 1] = 15;
            s[4, 3, 2] = 0;
            s[4, 3, 3] = 6;
            s[4, 3, 4] = 10;
            s[4, 3, 5] = 1;
            s[4, 3, 6] = 13;
            s[4, 3, 7] = 8;
            s[4, 3, 8] = 9;

            s[4, 3, 9] = 4;
            s[4, 3, 10] = 5;
            s[4, 3, 11] = 11;
            s[4, 3, 12] = 12;
            s[4, 3, 13] = 7;
            s[4, 3, 14] = 2;
            s[4, 3, 15] = 14;

            s[5, 0, 0] = 2;

            s[5, 0, 1] = 12;
            s[5, 0, 2] = 4;
            s[5, 0, 3] = 1;
            s[5, 0, 4] = 7;
            s[5, 0, 5] = 10;
            s[5, 0, 6] = 11;
            s[5, 0, 7] = 6;
            s[5, 0, 8] = 8;

            s[5, 0, 9] = 5;
            s[5, 0, 10] = 3;
            s[5, 0, 11] = 15;
            s[5, 0, 12] = 13;
            s[5, 0, 13] = 0;
            s[5, 0, 14] = 14;
            s[5, 0, 15] = 9;
            s[5, 1, 0] = 14;

            s[5, 1, 1] = 11;
            s[5, 1, 2] = 2;
            s[5, 1, 3] = 12;
            s[5, 1, 4] = 4;
            s[5, 1, 5] = 7;
            s[5, 1, 6] = 13;
            s[5, 1, 7] = 1;
            s[5, 1, 8] = 5;

            s[5, 1, 9] = 0;
            s[5, 1, 10] = 15;
            s[5, 1, 11] = 10;
            s[5, 1, 12] = 3;
            s[5, 1, 13] = 9;
            s[5, 1, 14] = 8;
            s[5, 1, 15] = 6;
            s[5, 2, 0] = 4;

            s[5, 2, 1] = 2;
            s[5, 2, 2] = 1;
            s[5, 2, 3] = 11;
            s[5, 2, 4] = 10;
            s[5, 2, 5] = 13;
            s[5, 2, 6] = 7;
            s[5, 2, 7] = 8;
            s[5, 2, 8] = 15;

            s[5, 2, 9] = 9;
            s[5, 2, 10] = 12;
            s[5, 2, 11] = 5;
            s[5, 2, 12] = 6;
            s[5, 2, 13] = 3;
            s[5, 2, 14] = 0;
            s[5, 2, 15] = 14;
            s[5, 3, 0] = 11;

            s[5, 3, 1] = 8;
            s[5, 3, 2] = 12;
            s[5, 3, 3] = 7;
            s[5, 3, 4] = 1;
            s[5, 3, 5] = 14;
            s[5, 3, 6] = 2;
            s[5, 3, 7] = 13;
            s[5, 3, 8] = 6;

            s[5, 3, 9] = 15;
            s[5, 3, 10] = 0;
            s[5, 3, 11] = 9;
            s[5, 3, 12] = 10;
            s[5, 3, 13] = 4;
            s[5, 3, 14] = 5;
            s[5, 3, 15] = 3;

            s[6, 0, 0] = 12;

            s[6, 0, 1] = 1;
            s[6, 0, 2] = 10;
            s[6, 0, 3] = 15;
            s[6, 0, 4] = 9;
            s[6, 0, 5] = 2;
            s[6, 0, 6] = 6;
            s[6, 0, 7] = 8;
            s[6, 0, 8] = 0;

            s[6, 0, 9] = 13;
            s[6, 0, 10] = 3;
            s[6, 0, 11] = 4;
            s[6, 0, 12] = 14;
            s[6, 0, 13] = 7;
            s[6, 0, 14] = 5;
            s[6, 0, 15] = 11;
            s[6, 1, 0] = 10;

            s[6, 1, 1] = 15;
            s[6, 1, 2] = 4;
            s[6, 1, 3] = 2;
            s[6, 1, 4] = 7;
            s[6, 1, 5] = 12;
            s[6, 1, 6] = 9;
            s[6, 1, 7] = 5;
            s[6, 1, 8] = 6;

            s[6, 1, 9] = 1;
            s[6, 1, 10] = 13;
            s[6, 1, 11] = 14;
            s[6, 1, 12] = 0;
            s[6, 1, 13] = 11;
            s[6, 1, 14] = 3;
            s[6, 1, 15] = 8;
            s[6, 2, 0] = 9;

            s[6, 2, 1] = 14;
            s[6, 2, 2] = 15;
            s[6, 2, 3] = 5;
            s[6, 2, 4] = 2;
            s[6, 2, 5] = 8;
            s[6, 2, 6] = 12;
            s[6, 2, 7] = 3;
            s[6, 2, 8] = 7;

            s[6, 2, 9] = 0;
            s[6, 2, 10] = 4;
            s[6, 2, 11] = 10;
            s[6, 2, 12] = 1;
            s[6, 2, 13] = 13;
            s[6, 2, 14] = 11;
            s[6, 2, 15] = 6;
            s[6, 3, 0] = 4;

            s[6, 3, 1] = 3;
            s[6, 3, 2] = 2;
            s[6, 3, 3] = 12;
            s[6, 3, 4] = 9;
            s[6, 3, 5] = 5;
            s[6, 3, 6] = 15;
            s[6, 3, 7] = 10;
            s[6, 3, 8] = 11;

            s[6, 3, 9] = 14;
            s[6, 3, 10] = 1;
            s[6, 3, 11] = 7;
            s[6, 3, 12] = 6;
            s[6, 3, 13] = 0;
            s[6, 3, 14] = 8;
            s[6, 3, 15] = 13;
            s[7, 0, 0] = 4;

            s[7, 0, 1] = 11;
            s[7, 0, 2] = 2;
            s[7, 0, 3] = 14;
            s[7, 0, 4] = 15;
            s[7, 0, 5] = 0;
            s[7, 0, 6] = 8;
            s[7, 0, 7] = 13;
            s[7, 0, 8] = 3;

            s[7, 0, 9] = 12;
            s[7, 0, 10] = 9;
            s[7, 0, 11] = 7;
            s[7, 0, 12] = 5;
            s[7, 0, 13] = 10;
            s[7, 0, 14] = 6;
            s[7, 0, 15] = 1;
            s[7, 1, 0] = 13;

            s[7, 1, 1] = 0;
            s[7, 1, 2] = 11;
            s[7, 1, 3] = 7;
            s[7, 1, 4] = 4;
            s[7, 1, 5] = 9;
            s[7, 1, 6] = 1;
            s[7, 1, 7] = 10;
            s[7, 1, 8] = 14;

            s[7, 1, 9] = 3;
            s[7, 1, 10] = 5;
            s[7, 1, 11] = 12;
            s[7, 1, 12] = 2;
            s[7, 1, 13] = 15;
            s[7, 1, 14] = 8;
            s[7, 1, 15] = 6;
            s[7, 2, 0] = 1;

            s[7, 2, 1] = 4;
            s[7, 2, 2] = 11;
            s[7, 2, 3] = 13;
            s[7, 2, 4] = 12;
            s[7, 2, 5] = 3;
            s[7, 2, 6] = 7;
            s[7, 2, 7] = 14;
            s[7, 2, 8] = 10;

            s[7, 2, 9] = 15;
            s[7, 2, 10] = 6;
            s[7, 2, 11] = 8;
            s[7, 2, 12] = 0;
            s[7, 2, 13] = 5;
            s[7, 2, 14] = 9;
            s[7, 2, 15] = 2;
            s[7, 3, 0] = 6;

            s[7, 3, 1] = 11;
            s[7, 3, 2] = 13;
            s[7, 3, 3] = 8;
            s[7, 3, 4] = 1;
            s[7, 3, 5] = 4;
            s[7, 3, 6] = 10;
            s[7, 3, 7] = 7;
            s[7, 3, 8] = 9;

            s[7, 3, 9] = 5;
            s[7, 3, 10] = 0;
            s[7, 3, 11] = 15;
            s[7, 3, 12] = 14;
            s[7, 3, 13] = 2;
            s[7, 3, 14] = 3;
            s[7, 3, 15] = 12;

            s[8, 0, 0] = 13;

            s[8, 0, 1] = 2;
            s[8, 0, 2] = 8;
            s[8, 0, 3] = 4;
            s[8, 0, 4] = 6;
            s[8, 0, 5] = 15;
            s[8, 0, 6] = 11;
            s[8, 0, 7] = 1;
            s[8, 0, 8] = 10;

            s[8, 0, 9] = 9;
            s[8, 0, 10] = 3;
            s[8, 0, 11] = 14;
            s[8, 0, 12] = 5;
            s[8, 0, 13] = 0;
            s[8, 0, 14] = 12;
            s[8, 0, 15] = 7;
            s[8, 1, 0] = 1;

            s[8, 1, 1] = 15;
            s[8, 1, 2] = 13;
            s[8, 1, 3] = 8;
            s[8, 1, 4] = 10;
            s[8, 1, 5] = 3;
            s[8, 1, 6] = 7;
            s[8, 1, 7] = 4;
            s[8, 1, 8] = 12;

            s[8, 1, 9] = 5;
            s[8, 1, 10] = 6;
            s[8, 1, 11] = 11;
            s[8, 1, 12] = 0;
            s[8, 1, 13] = 14;
            s[8, 1, 14] = 9;
            s[8, 1, 15] = 2;
            s[8, 2, 0] = 7;

            s[8, 2, 1] = 11;
            s[8, 2, 2] = 4;
            s[8, 2, 3] = 1;
            s[8, 2, 4] = 9;
            s[8, 2, 5] = 12;
            s[8, 2, 6] = 14;
            s[8, 2, 7] = 2;
            s[8, 2, 8] = 0;

            s[8, 2, 9] = 6;
            s[8, 2, 10] = 10;
            s[8, 2, 11] = 13;
            s[8, 2, 12] = 15;
            s[8, 2, 13] = 3;
            s[8, 2, 14] = 5;
            s[8, 2, 15] = 8;
            s[8, 3, 0] = 2;

            s[8, 3, 1] = 1;
            s[8, 3, 2] = 14;
            s[8, 3, 3] = 7;
            s[8, 3, 4] = 4;
            s[8, 3, 5] = 10;
            s[8, 3, 6] = 8;
            s[8, 3, 7] = 13;
            s[8, 3, 8] = 15;

            s[8, 3, 9] = 12;
            s[8, 3, 10] = 9;
            s[8, 3, 11] = 0;
            s[8, 3, 12] = 3;
            s[8, 3, 13] = 5;
            s[8, 3, 14] = 6;
            s[8, 3, 15] = 11;
        }


        /// <summary>
        ///Purpose:                           将bytToReplace中的数据按照bytReplaceWith
        ///                                   中的数值重新排列
        ///Input:                             bytToReplace()     所要置换的数组
        ///                                                      此数组必须是动态数组
        ///                                   bytReplaceWith()   置换的矩阵
        ///OutPut:                            bytToReplace()     置换后的数组
        ///Sample:
        ///  bitToReplace(15)  =0xEA,0xAA (11101010 10101010)
        ///  bytReplaceWith(15)=
        ///  (16,1,2,3,4,5,6,7
        ///    8,9,10,11,12,13,14,15)
        /// 将bitToReplace看作一个十六Bit的数组，转换后bitToReplace的第N-1位
        /// 等于原来bytToReplace的第bitReplaceWith(N)位，按上例，置换后的
        /// bytToReplace(1)= 0x705,0x55 (01110101 01010101)
        /// </summary>
        /// <param name="bitToReplace"></param>
        /// <param name="bytReplaceWith"></param>
        private void ReplaceArray(ref Bit[] bitToReplace, ref byte[] bytReplaceWith)
        {

            //Dim intReplaceTimes As Object '置换次数，共置换Ubound(bytReplaceWith)
            int intReplaceTimes; //置换次数，共置换Ubound(bytReplaceWith)
            int intCount; //循环变量
            Bit[] bitTmpArray;
            //UPGRADE_WARNING: 未能解析对象 intReplaceTimes 的默认属性。 单击以获得更多信息：'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
            intReplaceTimes = bytReplaceWith.GetUpperBound(0);
            //ReDim Preserve bitToReplace(intReplaceTimes)
            bitTmpArray = new Bit[intReplaceTimes];
            for (intCount = 0; intCount <= intReplaceTimes - 1; intCount++)
            {
                bitTmpArray[intCount] = bitToReplace[bytReplaceWith[intCount] - 1];
            }
            for (intCount = 0; intCount <= intReplaceTimes - 1; intCount++)
            {
                bitToReplace[intCount] = bitTmpArray[intCount];
            }
        }
        /// <summary>
        /// 由密钥bitKey(64)获得密钥方案
        /// KI(1)至KI(16)
        /// </summary>
        /// <param name="bitKey"> bitKey(64) as Bit    密钥</param>
        /// <returns>正确的密钥 ->True；错误的密钥 ->False</returns>
        private bool GetKi(ref Bit[] bitKey)
        {
            bool returnValue;
            try
            {
                int intCheckCount; //作奇偶校验使用
                Bit[] k = new Bit[57];
                Bit[] C = new Bit[29];
                Bit[] D = new Bit[29];
                int i;
                int j;
                int n;
                Bit tmpBit;
                Bit tmpBit1;
                Bit[] tmpBitAry = new Bit[57];
                Bit[] bitTmpKey = new Bit[57];
                //备份密钥
                for (i = 0; i <= 55; i++)
                {
                    bitTmpKey[i] = bitKey[i];
                }
                //维数检查
                if (bitKey.GetUpperBound(0) != 64 | bitKey.GetLowerBound(0) != 0)
                {
                    returnValue = false;
                    goto ExitFunc;
                }
                //奇偶校验
                n = 0;
                for (i = 0; i <= 7; i++)
                {
                    intCheckCount = 0;
                    for (j = 0; j <= 6; j++)
                    {
                        if (bitKey[i * 8 + j] == Bit.b1)
                        {
                            intCheckCount = intCheckCount + 1;
                        }
                        n = n + 1;
                    }
                    if ((intCheckCount % 2) != (1 - Convert.ToInt32(bitKey[i * 8 + 7])))
                    {
                        returnValue = false;
                        goto ExitFunc;
                    }
                }
                ReplaceArray(ref bitKey, ref PC1);
                for (i = 0; i <= 27; i++)
                {
                    C[i] = bitKey[i];
                    D[i] = bitKey[i + 28];
                }
                for (i = 1; i <= 16; i++)
                {
                    switch (i)
                    {
                        //循环左移一位
                        case 1:
                            tmpBit = C[0];
                            for (j = 0; j <= 26; j++)
                            {
                                C[j] = C[j + 1];
                            }
                            C[27] = tmpBit;
                            tmpBit = D[0];
                            for (j = 0; j <= 26; j++)
                            {
                                D[j] = D[j + 1];
                            }
                            D[27] = tmpBit;
                            break;
                        //循环左移两位

                        case 2:
                            tmpBit = C[0];
                            for (j = 0; j <= 26; j++)
                            {
                                C[j] = C[j + 1];
                            }
                            C[27] = tmpBit;
                            tmpBit = D[0];
                            for (j = 0; j <= 26; j++)
                            {
                                D[j] = D[j + 1];
                            }
                            D[27] = tmpBit;
                            break;
                        //循环左移两位

                        case 9:
                            tmpBit = C[0];
                            for (j = 0; j <= 26; j++)
                            {
                                C[j] = C[j + 1];
                            }
                            C[27] = tmpBit;
                            tmpBit = D[0];
                            for (j = 0; j <= 26; j++)
                            {
                                D[j] = D[j + 1];
                            }
                            D[27] = tmpBit;
                            break;
                        //循环左移两位

                        case 16:

                            tmpBit = C[0];
                            for (j = 0; j <= 26; j++)
                            {
                                C[j] = C[j + 1];
                            }
                            C[27] = tmpBit;
                            tmpBit = D[0];
                            for (j = 0; j <= 26; j++)
                            {
                                D[j] = D[j + 1];
                            }
                            D[27] = tmpBit;
                            break;
                        //循环左移两位
                        default:

                            tmpBit = C[0];
                            tmpBit1 = C[1];
                            for (j = 0; j <= 25; j++)
                            {
                                C[j] = C[j + 2];
                            }
                            C[26] = tmpBit;
                            C[27] = tmpBit1;
                            tmpBit = D[0];
                            tmpBit1 = D[1];
                            for (j = 0; j <= 25; j++)
                            {
                                D[j] = D[j + 2];
                            }
                            D[26] = tmpBit;
                            D[27] = tmpBit1;
                            break;
                    }
                    for (j = 0; j <= 27; j++)
                    {
                        k[j] = C[j];
                        k[j + 28] = D[j];
                    }
                    //ReplaceArray K(i)
                    for (j = 0; j <= 47; j++)
                    {
                        tmpBitAry[j] = (k[PC2[j] - 1]);
                    }
                    for (j = 0; j <= 47; j++)
                    {
                        KI[i, j] = tmpBitAry[j];
                    }
                }
                //还原密钥，防止其被改变
                for (i = 0; i <= 55; i++)
                {
                    bitKey[i] = bitTmpKey[i];
                }
                returnValue = true;
            ExitFunc:
                return returnValue;
            }
            catch
            {
                goto ErrorFunc;
            }

        ErrorFunc:
            returnValue = false;
            return returnValue;
        }
        private bool Cipher(ref Bit[] bitText, ref Bit[] bitKey)
        {
            bool returnValue;
            //Purpose:                   Cipher bitText(64) With Key bitKey(64)
            //Input:                     bitText(64) As Bit     Text to Cipher
            //                           bitKey(64) As Bit      Key to Cipher
            //OutPut                     bitText(64) As Bit     Text Ciphered
            //Return:                    正确的密钥 ->True
            //                           错误的密钥 ->False

            Bit[] L = new Bit[33]; //left part (32 bits) of text
            Bit[] R = new Bit[33]; //right part (32 bits) of text
            Bit[] C = new Bit[33]; //the value of T2 Changed by S Box
            Bit t; //temp variant
            Bit[] T1 = new Bit[33]; //the temp backup of L
            Bit[] T2 = new Bit[49]; //the temp backup of R
            int i; //loop variant
            int j;
            int Srow; //the row of S Box to use
            int Scol; //the col of S Box to use
            int Snum; //which S Box to use
            int Svalue; //the value in the correct s Box

            if (!GetKi(ref bitKey))
            {
                returnValue = false;
                goto ExitFunc;
            }
            //change text by IP
            ReplaceArray(ref bitText, ref IP);
            //equal text to L0 and R0
            for (i = 0; i <= 31; i++)
            {
                L[i] = bitText[i];
                R[i] = bitText[i + 32];
            }
            //loop 16 times to get the nonliner effect with S Box
            for (i = 1; i <= 16; i++)
            {
                //backup L & R
                for (j = 0; j <= 31; j++)
                {
                    T1[j] = L[j];
                    T2[j] = R[j];
                }
                //change T2(R') by function E
                ReplaceArray(ref T2, ref E);
                //change T2(R') by key
                for (j = 0; j <= 47; j++)
                {
                    T2[j] = T2[j] ^ KI[i, j];
                }
                //get C from both T2(R') and S Box
                for (j = 0; j <= 7; j++)
                {
                    Snum = j + 1;
                    Srow = (int)T2[j * 6] * 2 + (int)T2[j * 6 + 5];
                    Scol = (int)T2[j * 6 + 1] * 8 + (int)T2[j * 6 + 2] * 4 + (int)T2[j * 6 + 3] * 2 + (int)T2[j * 6 + 4];
                    Svalue = s[Snum, Srow, Scol];
                    C[j * 4] = (Svalue & 0x8) == 0 ? (Bit)0 : (Bit)1;
                    C[j * 4 + 1] = (Svalue & 0x4) == 0 ? (Bit)0 : (Bit)1;
                    C[j * 4 + 2] = (Svalue & 0x2) == 0 ? (Bit)0 : (Bit)1;
                    C[j * 4 + 3] = (Svalue & 0x1) == 0 ? (Bit)0 : (Bit)1;
                }
                //change C by function P
                ReplaceArray(ref C, ref P);
                //get L(i) = R(i-1)
                //    R(i) = L(i-1) Xor C
                for (j = 0; j <= 31; j++)
                {
                    t = L[j];
                    L[j] = R[j];
                    R[j] = t ^ C[j];
                }
            }
            //after 16 loops
            //get Text' =R(16)L(16)
            for (i = 0; i <= 31; i++)
            {
                bitText[i] = R[i];
                bitText[i + 32] = L[i];
            }
            //change Text' by  function IPC and finally get cryptograph
            ReplaceArray(ref bitText, ref IPC);
            returnValue = true;
        ExitFunc:
            1.GetHashCode(); //nop
            return returnValue;
        }
        private bool DeCipher(ref Bit[] bitCryptograph, ref Bit[] bitKey)
        {
            bool returnValue;
            //Purpose:                   Cipher bitCryptograph(64) With Key bitKey(64)
            //Input:                     bitCryptograph(64) As Bit     Cryptograph to DeCipher
            //                           bitKey(64) As Bit             Key to DeCipher
            //OutPut                     bitCryptograph(64) As Bit     Text DeCiphered
            //Return:                    正确的密钥 ->True
            //                           错误的密钥 ->False
            Bit[] L = new Bit[33]; //left part (32 bits) of Cryptograph
            Bit[] R = new Bit[33]; //right part (32 bits) of Cryptograph
            Bit[] C = new Bit[33]; //the value of T2 Changed by S Box
            Bit t; //temp variant
            Bit[] T1 = new Bit[33]; //the temp backup of L
            Bit[] T2 = new Bit[49]; //the temp backup of R
            short i; //loop variant
            short j;
            short Srow; //the row of S Box to use
            short Scol; //the col of S Box to use
            short Snum; //which S Box to use
            short Svalue; //the value in the correct s Box

            if (!GetKi(ref bitKey))
            {
                returnValue = false;
                goto ExitFunc;
            }
            //change text by IP
            ReplaceArray(ref bitCryptograph, ref IP);
            //equal text to L0 and R0
            for (i = 0; i <= 31; i++)
            {
                L[i] = bitCryptograph[i];
                R[i] = bitCryptograph[i + 32];
            }
            //loop 16 times to Lose the nonliner effect with S Box
            for (i = 1; i <= 16; i++)
            {
                //backup L & R
                for (j = 0; j <= 31; j++)
                {
                    T1[j] = L[j];
                    T2[j] = R[j];
                }
                //change T2(R') by function E
                ReplaceArray(ref T2, ref E);
                //change T2(R') by key
                for (j = 0; j <= 47; j++)
                {
                    //*****************************
                    //若在此处使用KI(i,j) 则是加密
                    //而现在则是解密
                    //这是加密和解密的唯一不同之处
                    //*****************************
                    T2[j] = T2[j] ^ KI[17 - i, j];
                }
                //get C from both T2(R') and S Box
                for (j = 0; j <= 7; j++)
                {
                    Snum = (short)(j + 1);
                    Srow = (short)((int)T2[j * 6] * 2 + (int)T2[j * 6 + 5]);
                    Scol = (short)((int)T2[j * 6 + 1] * 8 + (int)T2[j * 6 + 2] * 4 + (int)T2[j * 6 + 3] * 2 + (int)T2[j * 6 + 4]);
                    Svalue = s[Snum, Srow, Scol];
                    C[j * 4] = (Svalue & 0x8) == 0 ? (Bit)0 : (Bit)1;
                    C[j * 4 + 1] = (Svalue & 0x4) == 0 ? (Bit)0 : (Bit)1;
                    C[j * 4 + 2] = (Svalue & 0x2) == 0 ? (Bit)0 : (Bit)1;
                    C[j * 4 + 3] = (Svalue & 0x1) == 0 ? (Bit)0 : (Bit)1;
                }
                //change C by function P
                ReplaceArray(ref C, ref P);
                //get L(i) = R(i-1)
                //    R(i) = L(i-1) Xor C
                for (j = 0; j <= 31; j++)
                {
                    t = L[j];
                    L[j] = R[j];
                    R[j] = t ^ C[j];
                }
            }
            //after 16 loops
            //get Text' =R(16)L(16)
            for (i = 0; i <= 31; i++)
            {
                bitCryptograph[i] = R[i];
                bitCryptograph[i + 32] = L[i];
            }
            //change Cryptograph' by  function IPC and finally get Text
            ReplaceArray(ref bitCryptograph, ref IPC);
            returnValue = true;
        ExitFunc:
            1.GetHashCode(); //nop
            return returnValue;
        }
        /// <summary>
        ///  获得一个随机生成的64位密钥串
        ///  此密钥分为8位*8组，每一组的最低
        ///  位是奇偶校验位，采用奇校验。
        /// </summary>
        /// <returns></returns>
        public string GetRandomKey()
        {
            string returnValue = "";
            byte[] bytKey = new byte[8]; //密钥数组
            string StrKey = ""; //密钥字符串
            byte i; //循环变量
            byte j;
            int intCheckCount; //奇偶校验计数器
            Random Rnd = new Random();
            string tmpstr = "";
            for (i = 0; i <= 7; i++)
            {
                intCheckCount = 0; //奇偶校验计数器清0
                for (j = 1; j <= 6; j++)
                {
                    if (Rnd.NextDouble() < 0.5) //产生为0
                    {
                        tmpstr = (Convert.ToInt32(bytKey[i].ToString("x")) & (~(Convert.ToInt32((Rnd.Next(25) * 2) ^ j)))).ToString();
                        if (tmpstr.Length > 2)
                        { tmpstr = tmpstr.Substring(tmpstr.Length - 2, 2); }
                        bytKey[i] = Byte.Parse(Convert.ToInt32(tmpstr, 16).ToString());
                    }
                    else //产生为1
                    {
                        tmpstr = (Convert.ToInt32(bytKey[i].ToString("x")) | System.Convert.ToInt32((Rnd.Next(25) * 2) ^ j)).ToString();
                        if (tmpstr.Length > 2)
                        { tmpstr = tmpstr.Substring(tmpstr.Length - 2, 2); }
                        bytKey[i] = Byte.Parse(Convert.ToInt32(tmpstr, 16).ToString());
                        intCheckCount = intCheckCount + 1;
                    }
                }
                //添加奇偶校验位(最高位)使得每个字节含有奇数个1
                if ((intCheckCount % 2) == 1)
                {
                    bytKey[i] = Byte.Parse(((bytKey[i] * 2) & 0xFE).ToString());
                }
                else
                {
                    bytKey[i] = Byte.Parse(((bytKey[i] * 2) | 0x1).ToString());
                }
                //将密钥数组转化为字符串输出
                tmpstr = (bytKey[i].ToString("x")).ToString();
                if (tmpstr.Length > 2)
                { tmpstr = tmpstr.Substring(tmpstr.Length - 2, 2); }
                StrKey = StrKey + tmpstr;
                if (i < 7)
                {
                    StrKey = StrKey + " ";
                }
            }
            string[] aryKey; //存放密钥的数组
            aryKey = StrKey.Split(" ".ToCharArray(), 8);
            Bit[] bitKey = new Bit[65]; //转成二进制的密钥
            for (i = 0; i <= 7; i++)
            {
                int w1 = Convert.ToByte(aryKey[i], 16);
                bitKey[i * 8] = (Convert.ToByte(w1) & 0xF80) == 0 ? (Bit)0 : (Bit)1;
                // j = (int)bitKey[i * 8];
                //					bitKey[i * 8 + 1] =(Convert.ToByte(w1) & 0xF40)==1? (Bit)1: (Bit)0;
                bitKey[i * 8 + 1] = (Convert.ToByte(w1) & 0xF40) == 0 ? (Bit)0 : (Bit)1;
                bitKey[i * 8 + 2] = (Convert.ToByte(w1) & 0xF20) == 0 ? (Bit)0 : (Bit)1;
                bitKey[i * 8 + 3] = (Convert.ToByte(w1) & 0xF10) == 0 ? (Bit)0 : (Bit)1;
                bitKey[i * 8 + 4] = (Convert.ToByte(w1) & 0xF08) == 0 ? (Bit)0 : (Bit)1;
                bitKey[i * 8 + 5] = (Convert.ToByte(w1) & 0xF04) == 0 ? (Bit)0 : (Bit)1;
                bitKey[i * 8 + 6] = (Convert.ToByte(w1) & 0xF02) == 0 ? (Bit)0 : (Bit)1;
                bitKey[i * 8 + 7] = (Convert.ToByte(w1) & 0xF01) == 0 ? (Bit)0 : (Bit)1;
            }
            if (GetKi(ref bitKey))
            {
                returnValue = StrKey;
            }
            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tmpKey"></param>
        /// <returns></returns>
        public bool ChKey(string tmpKey)
        {
            bool returnValue = false;
            string[] aryKey; //存放密钥的数组
            aryKey = tmpKey.Split(" ".ToCharArray(), 8);
            Bit[] bitKey = new Bit[65]; //转成二进制的密钥
            for (int i = 0; i <= 7; i++)
            {
                int w1 = Convert.ToByte(aryKey[i], 16);
                bitKey[i * 8] = (Convert.ToByte(w1) & 0xF80) == 0 ? (Bit)0 : (Bit)1;
                // j = (int)bitKey[i * 8];
                //					bitKey[i * 8 + 1] =(Convert.ToByte(w1) & 0xF40)==1? (Bit)1: (Bit)0;
                bitKey[i * 8 + 1] = (Convert.ToByte(w1) & 0xF40) == 0 ? (Bit)0 : (Bit)1;
                bitKey[i * 8 + 2] = (Convert.ToByte(w1) & 0xF20) == 0 ? (Bit)0 : (Bit)1;
                bitKey[i * 8 + 3] = (Convert.ToByte(w1) & 0xF10) == 0 ? (Bit)0 : (Bit)1;
                bitKey[i * 8 + 4] = (Convert.ToByte(w1) & 0xF08) == 0 ? (Bit)0 : (Bit)1;
                bitKey[i * 8 + 5] = (Convert.ToByte(w1) & 0xF04) == 0 ? (Bit)0 : (Bit)1;
                bitKey[i * 8 + 6] = (Convert.ToByte(w1) & 0xF02) == 0 ? (Bit)0 : (Bit)1;
                bitKey[i * 8 + 7] = (Convert.ToByte(w1) & 0xF01) == 0 ? (Bit)0 : (Bit)1;
            }
            if (GetKi(ref bitKey))
            {
                returnValue = true;
            }
            return returnValue;
        }
        /// <summary>
        /// 用于加密
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="StrKey"></param>
        /// <returns></returns>
        public bool StringCipher(ref string strText, string StrKey)
        {
            bool returnValue;
            //Purpose:                           用密钥采用DES算法对明文加密，产生密文
            //Input:                             StrText As String      明文
            //                                   StrKey  As String      密钥
            //Output:                            StrText As String      密文
            try
            {
                byte[] bytPartText = new byte[9];
                string strCryptograph = ""; //密文
                string[] aryKey; //存放密钥的数组
                Bit[] bitKey = new Bit[65]; //转成二进制的密钥
                int intTextNumber; //明文共有多少组(每组8 byte)
                Bit[] bitText = new Bit[65];
                byte[] bytText;
                Bit[] bitCryptograph = new Bit[65];
                byte XByte;
                int j;
                int i;
                int k;
                short m;
                byte[] byt1;
                byte[] bytt1;
                //初始化参数
                InitParam();
                //获得密钥方案
                aryKey = StrKey.Split(" ".ToCharArray(), 8);
                for (i = 0; i <= 7; i++)
                {
                    int w1 = Convert.ToByte(aryKey[i], 16);
                    bitKey[i * 8] = (Convert.ToByte(w1) & 0xF80) == 0 ? (Bit)0 : (Bit)1;
                    j = (int)bitKey[i * 8];
                    //					bitKey[i * 8 + 1] =(Convert.ToByte(w1) & 0xF40)==1? (Bit)1: (Bit)0;
                    bitKey[i * 8 + 1] = (Convert.ToByte(w1) & 0xF40) == 0 ? (Bit)0 : (Bit)1;
                    bitKey[i * 8 + 2] = (Convert.ToByte(w1) & 0xF20) == 0 ? (Bit)0 : (Bit)1;
                    bitKey[i * 8 + 3] = (Convert.ToByte(w1) & 0xF10) == 0 ? (Bit)0 : (Bit)1;
                    bitKey[i * 8 + 4] = (Convert.ToByte(w1) & 0xF08) == 0 ? (Bit)0 : (Bit)1;
                    bitKey[i * 8 + 5] = (Convert.ToByte(w1) & 0xF04) == 0 ? (Bit)0 : (Bit)1;
                    bitKey[i * 8 + 6] = (Convert.ToByte(w1) & 0xF02) == 0 ? (Bit)0 : (Bit)1;
                    bitKey[i * 8 + 7] = (Convert.ToByte(w1) & 0xF01) == 0 ? (Bit)0 : (Bit)1;
                }
                if (!GetKi(ref bitKey))
                {
                    returnValue = false;
                    goto ExitFunc;
                }
                //按照ANSI规则处理明文
                //bytText = System.Text.UnicodeEncoding.Unicode.GetBytes(StrConv(strText, vbFromUnicode))
                //				bytText = System.Text.UnicodeEncoding.Unicode.GetBytes(strText);
                bytt1 = System.Text.UnicodeEncoding.Unicode.GetBytes(strText);
                bytText = new byte[bytt1.GetUpperBound(0) + 1];
                //				string we="";
                for (int y = 0; y <= bytt1.GetUpperBound(0); y++)
                {
                    bytText[y] = bytt1[y];
                }
                i = bytText.GetUpperBound(0) + 1;

                //最少cnLeastLength个Byte
                byt1 = new byte[i];
                int q = 0;
                int w = 0;
                int s = 0;
                if (i < cnLeastLength)
                {

                    for (q = 0; q < i; q++)
                    {
                        byt1[q] = bytText[q];

                    }
                    s = i;
                    bytText = new byte[cnLeastLength];
                    for (w = 0; w < cnLeastLength; w++)
                    {
                        if (w < s)
                        {
                            bytText[w] = byt1[w];
                        }
                        else
                        {
                            bytText[w] = 0;
                        }
                    }
                }
                else
                {
                    //且是8的倍数，否则补Asc(0)
                    if (((bytText.GetUpperBound(0) + 1) % 8) != 0)
                    {
                        for (i = (((bytText.GetUpperBound(0) + 1) % 8) - 1); i >= 0; i--)
                        {
                            //							bytText[bytText.GetUpperBound(0)] = 0;
                            //							bytText =new byte[bytText.GetUpperBound(0) + 1];
                        }
                    }
                }
                intTextNumber = (bytText.GetUpperBound(0) + 1) / 8 - 1;
                //取一块明文,转为二进制
                byte bytAsc;
                short intX;
                for (i = 0; i <= intTextNumber; i++)
                {
                    for (j = 0; j <= 7; j++)
                    {
                        bytAsc = bytText[i * 8 + j];
                        intX = 0x80;
                        for (k = 0; k <= 7; k++)
                        {
                            bitText[j * 8 + k] = (bytAsc & intX) == 0 ? (Bit)0 : (Bit)1;
                            intX = (short)(intX / 2);
                        }

                    }
                    //用CBC转换这组明文(用前一组密文和这一组明文XOR成这一组明文）
                    if (i != 0)
                    {
                        for (m = 0; m <= 63; m++)
                        {
                            bitText[m] = bitText[m] ^ bitCryptograph[m];
                        }
                    }
                    if (!Cipher(ref bitText, ref bitKey))
                    {
                        returnValue = false;
                        goto ExitFunc;
                    }
                    for (m = 0; m <= 7; m++)
                    {
                        XByte = 0;
                        for (j = 0; j <= 7; j++)
                        {
                            XByte = (byte)(XByte * 2 + bitText[m * 8 + j]);
                            //保留前一组密文
                            bitCryptograph[m * 8 + j] = bitText[m * 8 + j];
                        }
                        if ((XByte).ToString("x").Length == 1)
                        {
                            strCryptograph = strCryptograph + "0" + (XByte).ToString("x") + " ";
                        }
                        else
                        {
                            strCryptograph = strCryptograph + (XByte).ToString("x") + " ";
                        }
                    }
                }
                strText = strCryptograph;
                returnValue = true;
            ExitFunc:
                return returnValue;
            }
            catch
            {
                goto ErrorFunc;
            }

        ErrorFunc:
            returnValue = false;
            return returnValue;
        }
        /// <summary>
        /// 用于解密
        /// </summary>
        /// <param name="strText">需要加密的字符返回加密后的</param>
        /// <param name="StrKey">密钥</param>
        /// <returns></returns>
        public bool StringDecipher(ref string strText, string StrKey)
        {
            bool returnValue;
            try
            {
                byte[] bytPartText = new byte[9];
                string[] strCryptograph;
                byte[] bytCryptograph; //密文
                 
                string[] aryKey; //存放密钥的数组
                Bit[] bitKey = new Bit[65]; //转成二进制的密钥
                short intTextNumber; //明文共有多少组(每组8 byte)
                byte[] bytText;
                Bit[] bitCryptograph = new Bit[65];
                byte XByte;
                Bit[] bitLastCryptograph = new Bit[65];
                Bit[] bitText = new Bit[65];
                int j;
                int i;
                int k;
                short m;
                int w2 = 0;
                int l = 0;
                Bit[] bittemp = new Bit[65];
                //初始化参数
                InitParam();
                //获得密钥方案
                aryKey = StrKey.Split(" ".ToCharArray(), 8);
                for (i = 0; i <= 7; i++)
                {
                    int w1 = Convert.ToByte(aryKey[i], 16);
                    bitKey[i * 8] = (Convert.ToInt32(w1) & 0xF80) == 0 ? (Bit)0 : (Bit)1;
                    bitKey[i * 8 + 1] = (Convert.ToInt32(w1) & 0xF40) == 0 ? (Bit)0 : (Bit)1;
                    bitKey[i * 8 + 2] = (Convert.ToInt32(w1) & 0xF20) == 0 ? (Bit)0 : (Bit)1;
                    bitKey[i * 8 + 3] = (Convert.ToInt32(w1) & 0xF10) == 0 ? (Bit)0 : (Bit)1;
                    bitKey[i * 8 + 4] = (Convert.ToInt32(w1) & 0xF08) == 0 ? (Bit)0 : (Bit)1;
                    bitKey[i * 8 + 5] = (Convert.ToInt32(w1) & 0xF04) == 0 ? (Bit)0 : (Bit)1;
                    bitKey[i * 8 + 6] = (Convert.ToInt32(w1) & 0xF02) == 0 ? (Bit)0 : (Bit)1;
                    bitKey[i * 8 + 7] = (Convert.ToInt32(w1) & 0xF01) == 0 ? (Bit)0 : (Bit)1;
                }
                if (!GetKi(ref bitKey))
                {
                    returnValue = false;
                    goto ExitFunc;
                }
                strCryptograph = strText.Trim().Split(" ".ToCharArray());
                if (strCryptograph.GetUpperBound(0) + 1 < cnLeastLength || ((strCryptograph.GetUpperBound(0) + 1) % 8) != 0)
                {
                    returnValue = false;
                    goto ExitFunc;
                }
                bytText = new byte[strCryptograph.GetUpperBound(0) + 1];
                for (i = 0; i <= strCryptograph.GetUpperBound(0); i++)
                {
                    string strCC = "0x" + strCryptograph[i];
                    bool yn = true;
                    if (yn == false)
                    {
                        returnValue = false;
                        goto ExitFunc;
                    }
                    else
                    {
                        //						bytText[i] = Byte.Parse("0x" + strCryptograph[i]);
                        w2 = Convert.ToByte(strCryptograph[i], 16);
                        bytText[i] = (byte)w2;
                    }
                }
                intTextNumber = (short)((bytText.GetUpperBound(0) + 1) / 8 - 1);
                bytCryptograph = new byte[129];
                //取一块密文
                byte bytAsc;
                short intX;
                for (i = 0; i <= intTextNumber; i++)
                {
                    //转为二进制
                    for (j = 0; j <= 7; j++)
                    {
                        bytAsc = bytText[i * 8 + j];
                        intX = 0x80;
                        for (k = 0; k <= 7; k++)
                        {
                            bitText[j * 8 + k] = (bytAsc & intX) == 0 ? (Bit)0 : (Bit)1;
                            //备份本组密文
                            bittemp[j * 8 + k] = bitText[j * 8 + k];
                            intX = (short)(intX / 2);
                        }
                    }
                    //解密此组密文
                    if (!DeCipher(ref bitText, ref bitKey))
                    {
                        returnValue = false;
                        goto ExitFunc;
                    }
                    if (i != 0)
                    {
                        for (j = 0; j <= 63; j++)
                        {
                            bitText[j] = bitText[j] ^ bitLastCryptograph[j];
                        }
                    }
                    for (m = 0; m <= 7; m++)
                    {
                        XByte = 0;
                        for (j = 0; j <= 7; j++)
                        {
                            XByte = Convert.ToByte(XByte * 2 + bitText[m * 8 + j]);
                        }
                        //记载明文                       
                        l = i * 8 + m;
                        bytCryptograph[i * 8 + m] = XByte;
                    }
                    //备份本组密文，供下组解密使用
                    for (m = 0; m <= 63; m++)
                    {
                        bitLastCryptograph[m] = bittemp[m];
                    }
                }

                //将明文转化为字符串             
                strText = System.Text.UnicodeEncoding.Unicode.GetString(bytCryptograph);
                //去除后缀的Chr(0)
                string tee = strText.Substring(strText.Length - 1, 1);
                do
                {
                    strText = strText.Substring(0, strText.Length - 1);
                    if (strText == "0")
                    {
                        break;
                    }
                } while (strText.Substring(strText.Length - 1, 1) == "\0");
                return true;
            ExitFunc:
                return returnValue;
            }
            catch
            {
                goto ErrorFunc;
            }

        ErrorFunc:
            returnValue = false;
            return returnValue;
        }
    }
}
