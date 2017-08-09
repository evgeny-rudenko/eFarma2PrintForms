using ePlus.CommonEx.Controls;
namespace TORiglaEx
{
    partial class FormParams
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ucPeriod1 = new ePlus.MetaData.Client.UCPeriod();
            this.ucContractor = new ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chbFilteredMove = new System.Windows.Forms.CheckBox();
            this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxInvFromAP = new System.Windows.Forms.CheckBox();
            this.checkBoxRevaluation = new System.Windows.Forms.CheckBox();
            this.checkBoxDismantling = new System.Windows.Forms.CheckBox();
            this.checkBoxMoveInContr = new System.Windows.Forms.CheckBox();
            this.checkBoxComplaint = new System.Windows.Forms.CheckBox();
            this.checkBoxBackSale = new System.Windows.Forms.CheckBox();
            this.checkBoxWriteOff = new System.Windows.Forms.CheckBox();
            this.checkBoxShortageByInv = new System.Windows.Forms.CheckBox();
            this.checkBoxRecipesGrossDiscount = new System.Windows.Forms.CheckBox();
            this.checkBoxCash = new System.Windows.Forms.CheckBox();
            this.checkBoxCashless = new System.Windows.Forms.CheckBox();
            this.checkBoxRecipesGross = new System.Windows.Forms.CheckBox();
            this.checkBoxReturnToContrac = new System.Windows.Forms.CheckBox();
            this.checkBoxSK = new System.Windows.Forms.CheckBox();
            this.checkBoxExpensDiscount = new System.Windows.Forms.CheckBox();
            this.checkBoxService = new System.Windows.Forms.CheckBox();
            this.checkBoxReturnBuyer = new System.Windows.Forms.CheckBox();
            this.checkBoxReceipts = new System.Windows.Forms.CheckBox();
            this.checkBoxExcessByInvent = new System.Windows.Forms.CheckBox();
            this.checkBoxInvContrNWVat = new System.Windows.Forms.CheckBox();
            this.checkBoxExpens = new System.Windows.Forms.CheckBox();
            this.checkBoxInv = new System.Windows.Forms.CheckBox();
            this.checkBoxInvContrWVat = new System.Windows.Forms.CheckBox();
            this.checkBoxInvRemainder = new System.Windows.Forms.CheckBox();
            this.checkBoxUseDiagnReport = new System.Windows.Forms.CheckBox();
            this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(171, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(246, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 463);
            this.panel1.Size = new System.Drawing.Size(324, 29);
            // 
            // ucPeriod1
            // 
            this.ucPeriod1.DateFrom = new System.DateTime(2009, 5, 13, 10, 45, 27, 781);
            this.ucPeriod1.DateTo = new System.DateTime(2009, 5, 13, 10, 45, 27, 781);
            this.ucPeriod1.Location = new System.Drawing.Point(66, 9);
            this.ucPeriod1.Name = "ucPeriod1";
            this.ucPeriod1.Size = new System.Drawing.Size(230, 21);
            this.ucPeriod1.TabIndex = 4;
            // 
            // ucContractor
            // 
            this.ucContractor.ClearTextOnValidatingIfValueIsEmpty = true;
            this.ucContractor.ELikeTextOption = ePlus.MetaData.Core.ELikeTextOption.None;
            this.ucContractor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic);
            this.ucContractor.Location = new System.Drawing.Point(45, 45);
            this.ucContractor.Name = "ucContractor";
            this.ucContractor.PluginMnemocode = "CONTRACTOR";
            this.ucContractor.SelectNextControlAfterSelectEntity = false;
            this.ucContractor.Size = new System.Drawing.Size(269, 20);
            this.ucContractor.TabIndex = 8;
            this.ucContractor.UseEnterToOpenPlugin = true;
            this.ucContractor.UseSpaceToOpenPlugin = true;
            this.ucContractor.TextChanged += new System.EventHandler(this.ucContractor_TextChanged);
            this.ucContractor.ValueChanged += new System.EventHandler(this.ucContractor_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Период:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "АУ:";
            // 
            // chbFilteredMove
            // 
            this.chbFilteredMove.AutoSize = true;
            this.chbFilteredMove.Location = new System.Drawing.Point(7, 432);
            this.chbFilteredMove.Name = "chbFilteredMove";
            this.chbFilteredMove.Size = new System.Drawing.Size(234, 17);
            this.chbFilteredMove.TabIndex = 9;
            this.chbFilteredMove.Text = "Отфильтровать перемещения внутри АУ";
            this.chbFilteredMove.UseVisualStyleBackColor = true;
            // 
            // ucStore
            // 
            this.ucStore.AllowSaveState = false;
            this.ucStore.Caption = "Склад";
            this.ucStore.Location = new System.Drawing.Point(7, 325);
            this.ucStore.Mnemocode = "STORE";
            this.ucStore.Name = "ucStore";
            //this.ucStore.Pinnable = false;
            this.ucStore.Size = new System.Drawing.Size(310, 101);
            this.ucStore.TabIndex = 10;
            this.ucStore.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucStore_BeforePluginShow);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxInvFromAP);
            this.groupBox1.Controls.Add(this.checkBoxRevaluation);
            this.groupBox1.Controls.Add(this.checkBoxDismantling);
            this.groupBox1.Controls.Add(this.checkBoxMoveInContr);
            this.groupBox1.Controls.Add(this.checkBoxComplaint);
            this.groupBox1.Controls.Add(this.checkBoxBackSale);
            this.groupBox1.Controls.Add(this.checkBoxWriteOff);
            this.groupBox1.Controls.Add(this.checkBoxShortageByInv);
            this.groupBox1.Controls.Add(this.checkBoxRecipesGrossDiscount);
            this.groupBox1.Controls.Add(this.checkBoxCash);
            this.groupBox1.Controls.Add(this.checkBoxCashless);
            this.groupBox1.Controls.Add(this.checkBoxRecipesGross);
            this.groupBox1.Controls.Add(this.checkBoxReturnToContrac);
            this.groupBox1.Controls.Add(this.checkBoxSK);
            this.groupBox1.Controls.Add(this.checkBoxExpensDiscount);
            this.groupBox1.Controls.Add(this.checkBoxService);
            this.groupBox1.Controls.Add(this.checkBoxReturnBuyer);
            this.groupBox1.Controls.Add(this.checkBoxReceipts);
            this.groupBox1.Controls.Add(this.checkBoxExcessByInvent);
            this.groupBox1.Controls.Add(this.checkBoxInvContrNWVat);
            this.groupBox1.Controls.Add(this.checkBoxExpens);
            this.groupBox1.Controls.Add(this.checkBoxInv);
            this.groupBox1.Controls.Add(this.checkBoxInvContrWVat);
            this.groupBox1.Controls.Add(this.checkBoxInvRemainder);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(323, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 432);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // checkBoxInvFromAP
            // 
            this.checkBoxInvFromAP.AutoSize = true;
            this.checkBoxInvFromAP.Location = new System.Drawing.Point(31, 106);
            this.checkBoxInvFromAP.Name = "checkBoxInvFromAP";
            this.checkBoxInvFromAP.Size = new System.Drawing.Size(109, 17);
            this.checkBoxInvFromAP.TabIndex = 25;
            this.checkBoxInvFromAP.Text = "Приход от аптек";
            this.checkBoxInvFromAP.UseVisualStyleBackColor = true;
            this.checkBoxInvFromAP.CheckedChanged += new System.EventHandler(this.checkBoxInvGroupChange);
            // 
            // checkBoxRevaluation
            // 
            this.checkBoxRevaluation.AutoSize = true;
            this.checkBoxRevaluation.Location = new System.Drawing.Point(31, 394);
            this.checkBoxRevaluation.Name = "checkBoxRevaluation";
            this.checkBoxRevaluation.Size = new System.Drawing.Size(88, 17);
            this.checkBoxRevaluation.TabIndex = 24;
            this.checkBoxRevaluation.Text = "Переоценка";
            this.checkBoxRevaluation.UseVisualStyleBackColor = true;
            this.checkBoxRevaluation.CheckedChanged += new System.EventHandler(this.checkBoxExpensGroupChange);
            // 
            // checkBoxDismantling
            // 
            this.checkBoxDismantling.AutoSize = true;
            this.checkBoxDismantling.Location = new System.Drawing.Point(31, 411);
            this.checkBoxDismantling.Name = "checkBoxDismantling";
            this.checkBoxDismantling.Size = new System.Drawing.Size(75, 17);
            this.checkBoxDismantling.TabIndex = 23;
            this.checkBoxDismantling.Text = "Разборка";
            this.checkBoxDismantling.UseVisualStyleBackColor = true;
            this.checkBoxDismantling.CheckedChanged += new System.EventHandler(this.checkBoxExpensGroupChange);
            // 
            // checkBoxMoveInContr
            // 
            this.checkBoxMoveInContr.AutoSize = true;
            this.checkBoxMoveInContr.Location = new System.Drawing.Point(31, 344);
            this.checkBoxMoveInContr.Name = "checkBoxMoveInContr";
            this.checkBoxMoveInContr.Size = new System.Drawing.Size(146, 17);
            this.checkBoxMoveInContr.TabIndex = 21;
            this.checkBoxMoveInContr.Text = "Перемещение в аптеки";
            this.checkBoxMoveInContr.UseVisualStyleBackColor = true;
            this.checkBoxMoveInContr.CheckedChanged += new System.EventHandler(this.checkBoxExpensGroupChange);
            // 
            // checkBoxComplaint
            // 
            this.checkBoxComplaint.AutoSize = true;
            this.checkBoxComplaint.Location = new System.Drawing.Point(58, 311);
            this.checkBoxComplaint.Name = "checkBoxComplaint";
            this.checkBoxComplaint.Size = new System.Drawing.Size(81, 17);
            this.checkBoxComplaint.TabIndex = 20;
            this.checkBoxComplaint.Text = "Притензии";
            this.checkBoxComplaint.UseVisualStyleBackColor = true;
            this.checkBoxComplaint.CheckedChanged += new System.EventHandler(this.checkBoxExpensGroupChange);
            // 
            // checkBoxBackSale
            // 
            this.checkBoxBackSale.AutoSize = true;
            this.checkBoxBackSale.Location = new System.Drawing.Point(58, 327);
            this.checkBoxBackSale.Name = "checkBoxBackSale";
            this.checkBoxBackSale.Size = new System.Drawing.Size(138, 17);
            this.checkBoxBackSale.TabIndex = 19;
            this.checkBoxBackSale.Text = "Обратная реализация";
            this.checkBoxBackSale.UseVisualStyleBackColor = true;
            this.checkBoxBackSale.CheckedChanged += new System.EventHandler(this.checkBoxExpensGroupChange);
            // 
            // checkBoxWriteOff
            // 
            this.checkBoxWriteOff.AutoSize = true;
            this.checkBoxWriteOff.Location = new System.Drawing.Point(31, 360);
            this.checkBoxWriteOff.Name = "checkBoxWriteOff";
            this.checkBoxWriteOff.Size = new System.Drawing.Size(75, 17);
            this.checkBoxWriteOff.TabIndex = 18;
            this.checkBoxWriteOff.Text = "Списание";
            this.checkBoxWriteOff.UseVisualStyleBackColor = true;
            this.checkBoxWriteOff.CheckedChanged += new System.EventHandler(this.checkBoxExpensGroupChange);
            // 
            // checkBoxShortageByInv
            // 
            this.checkBoxShortageByInv.AutoSize = true;
            this.checkBoxShortageByInv.Location = new System.Drawing.Point(31, 377);
            this.checkBoxShortageByInv.Name = "checkBoxShortageByInv";
            this.checkBoxShortageByInv.Size = new System.Drawing.Size(181, 17);
            this.checkBoxShortageByInv.TabIndex = 17;
            this.checkBoxShortageByInv.Text = "Недостачи по инвентаризации";
            this.checkBoxShortageByInv.UseVisualStyleBackColor = true;
            this.checkBoxShortageByInv.CheckedChanged += new System.EventHandler(this.checkBoxExpensGroupChange);
            // 
            // checkBoxRecipesGrossDiscount
            // 
            this.checkBoxRecipesGrossDiscount.AutoSize = true;
            this.checkBoxRecipesGrossDiscount.Location = new System.Drawing.Point(58, 278);
            this.checkBoxRecipesGrossDiscount.Name = "checkBoxRecipesGrossDiscount";
            this.checkBoxRecipesGrossDiscount.Size = new System.Drawing.Size(63, 17);
            this.checkBoxRecipesGrossDiscount.TabIndex = 16;
            this.checkBoxRecipesGrossDiscount.Text = "Скидки";
            this.checkBoxRecipesGrossDiscount.UseVisualStyleBackColor = true;
            this.checkBoxRecipesGrossDiscount.CheckedChanged += new System.EventHandler(this.checkBoxRecipesGrossDiscount_CheckedChanged);
            // 
            // checkBoxCash
            // 
            this.checkBoxCash.AutoSize = true;
            this.checkBoxCash.Location = new System.Drawing.Point(58, 228);
            this.checkBoxCash.Name = "checkBoxCash";
            this.checkBoxCash.Size = new System.Drawing.Size(75, 17);
            this.checkBoxCash.TabIndex = 15;
            this.checkBoxCash.Text = "Наличная";
            this.checkBoxCash.UseVisualStyleBackColor = true;
            this.checkBoxCash.CheckedChanged += new System.EventHandler(this.checkBoxExpensGroupChange);
            // 
            // checkBoxCashless
            // 
            this.checkBoxCashless.AutoSize = true;
            this.checkBoxCashless.Location = new System.Drawing.Point(58, 245);
            this.checkBoxCashless.Name = "checkBoxCashless";
            this.checkBoxCashless.Size = new System.Drawing.Size(92, 17);
            this.checkBoxCashless.TabIndex = 14;
            this.checkBoxCashless.Text = "Безналичная";
            this.checkBoxCashless.UseVisualStyleBackColor = true;
            this.checkBoxCashless.CheckedChanged += new System.EventHandler(this.checkBoxExpensGroupChange);
            // 
            // checkBoxRecipesGross
            // 
            this.checkBoxRecipesGross.AutoSize = true;
            this.checkBoxRecipesGross.Location = new System.Drawing.Point(31, 262);
            this.checkBoxRecipesGross.Name = "checkBoxRecipesGross";
            this.checkBoxRecipesGross.Size = new System.Drawing.Size(88, 17);
            this.checkBoxRecipesGross.TabIndex = 13;
            this.checkBoxRecipesGross.Text = "Расход (опт)";
            this.checkBoxRecipesGross.UseVisualStyleBackColor = true;
            this.checkBoxRecipesGross.CheckedChanged += new System.EventHandler(this.checkBoxRecipesGross_CheckedChanged);
            // 
            // checkBoxReturnToContrac
            // 
            this.checkBoxReturnToContrac.AutoSize = true;
            this.checkBoxReturnToContrac.Location = new System.Drawing.Point(31, 295);
            this.checkBoxReturnToContrac.Name = "checkBoxReturnToContrac";
            this.checkBoxReturnToContrac.Size = new System.Drawing.Size(132, 17);
            this.checkBoxReturnToContrac.TabIndex = 12;
            this.checkBoxReturnToContrac.Text = "Возврат поставщику";
            this.checkBoxReturnToContrac.UseVisualStyleBackColor = true;
            this.checkBoxReturnToContrac.CheckedChanged += new System.EventHandler(this.checkBoxReturnToContrac_CheckedChanged);
            // 
            // checkBoxSK
            // 
            this.checkBoxSK.AutoSize = true;
            this.checkBoxSK.Location = new System.Drawing.Point(84, 210);
            this.checkBoxSK.Name = "checkBoxSK";
            this.checkBoxSK.Size = new System.Drawing.Size(108, 17);
            this.checkBoxSK.TabIndex = 11;
            this.checkBoxSK.Text = "Операции со СК";
            this.checkBoxSK.UseVisualStyleBackColor = true;
            this.checkBoxSK.CheckedChanged += new System.EventHandler(this.checkBoxSK_CheckedChanged);
            // 
            // checkBoxExpensDiscount
            // 
            this.checkBoxExpensDiscount.AutoSize = true;
            this.checkBoxExpensDiscount.Location = new System.Drawing.Point(58, 193);
            this.checkBoxExpensDiscount.Name = "checkBoxExpensDiscount";
            this.checkBoxExpensDiscount.Size = new System.Drawing.Size(63, 17);
            this.checkBoxExpensDiscount.TabIndex = 10;
            this.checkBoxExpensDiscount.Text = "Скидки";
            this.checkBoxExpensDiscount.UseVisualStyleBackColor = true;
            this.checkBoxExpensDiscount.CheckedChanged += new System.EventHandler(this.checkBoxExpensDiscount_CheckedChanged);
            // 
            // checkBoxService
            // 
            this.checkBoxService.AutoSize = true;
            this.checkBoxService.Location = new System.Drawing.Point(58, 176);
            this.checkBoxService.Name = "checkBoxService";
            this.checkBoxService.Size = new System.Drawing.Size(62, 17);
            this.checkBoxService.TabIndex = 9;
            this.checkBoxService.Text = "Услуги";
            this.checkBoxService.UseVisualStyleBackColor = true;
            this.checkBoxService.CheckedChanged += new System.EventHandler(this.checkBoxExpensGroupChange);
            // 
            // checkBoxReturnBuyer
            // 
            this.checkBoxReturnBuyer.AutoSize = true;
            this.checkBoxReturnBuyer.Location = new System.Drawing.Point(31, 90);
            this.checkBoxReturnBuyer.Name = "checkBoxReturnBuyer";
            this.checkBoxReturnBuyer.Size = new System.Drawing.Size(143, 17);
            this.checkBoxReturnBuyer.TabIndex = 8;
            this.checkBoxReturnBuyer.Text = "Возврат от покупателя";
            this.checkBoxReturnBuyer.UseVisualStyleBackColor = true;
            this.checkBoxReturnBuyer.CheckedChanged += new System.EventHandler(this.checkBoxInvGroupChange);
            // 
            // checkBoxReceipts
            // 
            this.checkBoxReceipts.AutoSize = true;
            this.checkBoxReceipts.Location = new System.Drawing.Point(31, 158);
            this.checkBoxReceipts.Name = "checkBoxReceipts";
            this.checkBoxReceipts.Size = new System.Drawing.Size(69, 17);
            this.checkBoxReceipts.TabIndex = 7;
            this.checkBoxReceipts.Text = "Выручка";
            this.checkBoxReceipts.UseVisualStyleBackColor = true;
            this.checkBoxReceipts.CheckedChanged += new System.EventHandler(this.checkBoxReceipts_CheckedChanged);
            // 
            // checkBoxExcessByInvent
            // 
            this.checkBoxExcessByInvent.AutoSize = true;
            this.checkBoxExcessByInvent.Location = new System.Drawing.Point(31, 122);
            this.checkBoxExcessByInvent.Name = "checkBoxExcessByInvent";
            this.checkBoxExcessByInvent.Size = new System.Drawing.Size(173, 17);
            this.checkBoxExcessByInvent.TabIndex = 6;
            this.checkBoxExcessByInvent.Text = "Излишки по инвентаризации";
            this.checkBoxExcessByInvent.UseVisualStyleBackColor = true;
            this.checkBoxExcessByInvent.CheckedChanged += new System.EventHandler(this.checkBoxInvGroupChange);
            // 
            // checkBoxInvContrNWVat
            // 
            this.checkBoxInvContrNWVat.AutoSize = true;
            this.checkBoxInvContrNWVat.Location = new System.Drawing.Point(31, 73);
            this.checkBoxInvContrNWVat.Name = "checkBoxInvContrNWVat";
            this.checkBoxInvContrNWVat.Size = new System.Drawing.Size(261, 17);
            this.checkBoxInvContrNWVat.TabIndex = 5;
            this.checkBoxInvContrNWVat.Text = "Приход от поставщика (не плательщики НДС)";
            this.checkBoxInvContrNWVat.UseVisualStyleBackColor = true;
            this.checkBoxInvContrNWVat.CheckedChanged += new System.EventHandler(this.checkBoxInvGroupChange);
            // 
            // checkBoxExpens
            // 
            this.checkBoxExpens.AutoSize = true;
            this.checkBoxExpens.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxExpens.Location = new System.Drawing.Point(11, 140);
            this.checkBoxExpens.Name = "checkBoxExpens";
            this.checkBoxExpens.Size = new System.Drawing.Size(68, 17);
            this.checkBoxExpens.TabIndex = 4;
            this.checkBoxExpens.Text = "Расход";
            this.checkBoxExpens.UseVisualStyleBackColor = true;
            this.checkBoxExpens.CheckedChanged += new System.EventHandler(this.checkBoxExpens_CheckedChanged);
            // 
            // checkBoxInv
            // 
            this.checkBoxInv.AutoSize = true;
            this.checkBoxInv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxInv.Location = new System.Drawing.Point(11, 40);
            this.checkBoxInv.Name = "checkBoxInv";
            this.checkBoxInv.Size = new System.Drawing.Size(69, 17);
            this.checkBoxInv.TabIndex = 3;
            this.checkBoxInv.Text = "Приход";
            this.checkBoxInv.UseVisualStyleBackColor = true;
            this.checkBoxInv.CheckedChanged += new System.EventHandler(this.checkBoxInv_CheckedChanged);
            // 
            // checkBoxInvContrWVat
            // 
            this.checkBoxInvContrWVat.AutoSize = true;
            this.checkBoxInvContrWVat.Location = new System.Drawing.Point(31, 57);
            this.checkBoxInvContrWVat.Name = "checkBoxInvContrWVat";
            this.checkBoxInvContrWVat.Size = new System.Drawing.Size(246, 17);
            this.checkBoxInvContrWVat.TabIndex = 2;
            this.checkBoxInvContrWVat.Text = "Приход от поставщика (плательщики НДС)";
            this.checkBoxInvContrWVat.UseVisualStyleBackColor = true;
            this.checkBoxInvContrWVat.CheckedChanged += new System.EventHandler(this.checkBoxInvGroupChange);
            // 
            // checkBoxInvRemainder
            // 
            this.checkBoxInvRemainder.AutoSize = true;
            this.checkBoxInvRemainder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxInvRemainder.Location = new System.Drawing.Point(11, 23);
            this.checkBoxInvRemainder.Name = "checkBoxInvRemainder";
            this.checkBoxInvRemainder.Size = new System.Drawing.Size(136, 17);
            this.checkBoxInvRemainder.TabIndex = 1;
            this.checkBoxInvRemainder.Text = "Входящий остаток";
            this.checkBoxInvRemainder.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseDiagnReport
            // 
            this.checkBoxUseDiagnReport.AutoSize = true;
            this.checkBoxUseDiagnReport.Location = new System.Drawing.Point(327, 9);
            this.checkBoxUseDiagnReport.Name = "checkBoxUseDiagnReport";
            this.checkBoxUseDiagnReport.Size = new System.Drawing.Size(146, 17);
            this.checkBoxUseDiagnReport.TabIndex = 0;
            this.checkBoxUseDiagnReport.Text = "Диагностический отчет";
            this.checkBoxUseDiagnReport.UseVisualStyleBackColor = true;
            this.checkBoxUseDiagnReport.Visible = false;
            this.checkBoxUseDiagnReport.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ucGoods
            // 
            this.ucGoods.AllowSaveState = false;
            this.ucGoods.Caption = "Товары";
            this.ucGoods.Location = new System.Drawing.Point(7, 74);
            this.ucGoods.Mnemocode = "GOODS2";
            this.ucGoods.Name = "ucGoods";
            //this.ucGoods.Pinnable = false;
            this.ucGoods.Size = new System.Drawing.Size(310, 245);
            this.ucGoods.TabIndex = 17;
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 492);
            this.Controls.Add(this.ucGoods);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ucStore);
            this.Controls.Add(this.chbFilteredMove);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucPeriod1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucContractor);
            this.Controls.Add(this.checkBoxUseDiagnReport);
            this.Name = "FormParams";
            this.Load += new System.EventHandler(this.FormParams_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParams_FormClosed);
            this.Controls.SetChildIndex(this.checkBoxUseDiagnReport, 0);
            this.Controls.SetChildIndex(this.ucContractor, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ucPeriod1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.chbFilteredMove, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.ucStore, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.ucGoods, 0);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPeriod ucPeriod1;
        private MetaPluginDictionarySelectControl ucContractor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbFilteredMove;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxUseDiagnReport;
        private System.Windows.Forms.CheckBox checkBoxExpens;
        private System.Windows.Forms.CheckBox checkBoxInv;
        private System.Windows.Forms.CheckBox checkBoxInvContrWVat;
        private System.Windows.Forms.CheckBox checkBoxInvRemainder;
        private System.Windows.Forms.CheckBox checkBoxExpensDiscount;
        private System.Windows.Forms.CheckBox checkBoxService;
        private System.Windows.Forms.CheckBox checkBoxReturnBuyer;
        private System.Windows.Forms.CheckBox checkBoxReceipts;
        private System.Windows.Forms.CheckBox checkBoxExcessByInvent;
        private System.Windows.Forms.CheckBox checkBoxInvContrNWVat;
        private System.Windows.Forms.CheckBox checkBoxRecipesGrossDiscount;
        private System.Windows.Forms.CheckBox checkBoxCash;
        private System.Windows.Forms.CheckBox checkBoxCashless;
        private System.Windows.Forms.CheckBox checkBoxRecipesGross;
        private System.Windows.Forms.CheckBox checkBoxReturnToContrac;
        private System.Windows.Forms.CheckBox checkBoxSK;
        private System.Windows.Forms.CheckBox checkBoxComplaint;
        private System.Windows.Forms.CheckBox checkBoxBackSale;
        private System.Windows.Forms.CheckBox checkBoxWriteOff;
        private System.Windows.Forms.CheckBox checkBoxShortageByInv;
        private System.Windows.Forms.CheckBox checkBoxMoveInContr;
        private System.Windows.Forms.CheckBox checkBoxRevaluation;
        private System.Windows.Forms.CheckBox checkBoxDismantling;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
        private System.Windows.Forms.CheckBox checkBoxInvFromAP;

    }
}