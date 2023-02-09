import { Component } from '@angular/core';
import { ApiCallService } from './services/api-call.service'
import * as XLSX from 'xlsx';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AssignmentFirst';
  fileName = 'ExcelSheet.xlsx';
  item: any;
  dateFrom: Date = new Date(0);
  dateTo: Date = new Date(0);
  availData: any;
  radioValue: any;
  panelList: any;
  errorMsg: any;
  selectedMgr: [] = [];
  strValues: string = '';
  mgrNames: any;
  emptyListMgrId: any;
  mgrNotAvailMsg: any;
  noOfRecords: any;
  pageSize = 10;
  currentPage = 1;
  numberOfPages: number | undefined;
  noOfRecordsByDate: any;

  previousPage() {
    this.currentPage--;
  }
  nextPage() {
    this.currentPage++;
  }
  constructor(private trackerItem: ApiCallService) {
    trackerItem.trackerData().subscribe((data) => {
      console.warn('data', data);
      this.item = data;
      this.numberOfPages = Math.ceil(this.noOfRecords / this.pageSize);
    })
  }
  get startIndex(): number {
    return (this.currentPage - 1) * this.pageSize;
  }

  get endIndex(): number {
    return Math.min(this.startIndex + this.pageSize, this.noOfRecords);
  }
  ngOnInit() {
    this.numberOfPages = Math.ceil(this.noOfRecords / this.pageSize);
    this.previousPage()
    {
      this.currentPage--;
    }

    this.nextPage()
    {
      this.currentPage++;
    }
  }

  validateDate(dateFrom: Date, dateTo: Date) {
    if ((dateFrom.toString() != "" && dateTo.toString() != "" && dateTo >= dateFrom)) {
      return true
    }
    else if (dateFrom.toString() == "" || dateTo.toString() == "") {
      this.errorMsg = "Please enter both the dates!!!";
      return false
    }
    else {
      this.errorMsg = "'Date From' should be less than 'Date To'!!!"
      return false
    }
  }


  getAll() {
    this.trackerItem.trackerDates(this.dateFrom, this.dateTo).subscribe((data) => {
      console.warn(data)
      if (data != '') 
      { this.availData = data; } 
      else { 
        const format = 'dd-MM-yyyy'; 
        const locale = 'en-US'; 
        const formattedDateFrom = formatDate(this.dateFrom, format, locale);
         const formattedDateTo = formatDate(this.dateTo, format, locale);
          this.errorMsg = 'No Panel available between ' + formattedDateFrom + ' and ' + formattedDateTo }
      this.noOfRecordsByDate = this.availData.length;
      console.warn('Records by date ' + this.noOfRecordsByDate);
      this.currentPage = 1;

    })
  }

  getData(date1: any, date2: any) {
    if (this.validateDate(date1, date2)) {
      this.errorMsg="";
      console.warn(date1, date2);
      this.dateFrom = date1;
      this.dateTo = date2;
      console.warn(this.dateFrom + " " + this.dateTo);
      this.getAll();
    }

  }

  getPanelByMgr(MgrId: any) {
    console.warn('mgrID : ', MgrId)
    this.trackerItem.trackerPanelByMgr(MgrId).subscribe((panel) => {
      console.warn('panel : ', panel)
      this.panelList = panel;
      //this.panelList.
      // this.noOfRecords=this.panelList.length;
      // console.warn('No Of Records ',this.noOfRecords)
    })
  }


  getPanelByMultipleMgrs() {
    this.strValues = this.selectedMgr.toString();

    console.warn('strValues : ', this.strValues);

    this.trackerItem.trackerPanelByMultipleMgrs(this.strValues).subscribe((panel) => {
      console.warn('Multiple Panel', panel)
      this.panelList = panel;
      if (this.panelList == '') {
        this.emptyListMgrId = this.selectedMgr.toString();
        this.trackerItem.trackerEmptyListMgrName(this.emptyListMgrId).subscribe((mgrName) => {
          console.warn('mgrList', mgrName);
          this.mgrNames = mgrName;
        })
        this.mgrNotAvailMsg = 'No Panel Available under'
      }
      this.noOfRecords = this.panelList.length;
      this.noOfRecords = this.panelList.length; this.numberOfPages = Math.ceil(this.noOfRecords / this.pageSize);
      console.warn('No Of Records ', this.noOfRecords)
      this.currentPage = 1;
    })

  }

  // radioChangeHandler(event: any) {
  //   this.radioValue = event.target.value;
  // }
  radioChangeHandler(event: any) {
    this.radioValue = event.target.value;
    if (this.radioValue == 'byMgr' || this.radioValue == 'byDate') {
      this.errorMsg = "";
      this.availData = '';
      this.panelList = '';
      this.mgrNotAvailMsg = '';
      this.mgrNames = '';
      this.selectedMgr = [];

    }
  }

  exportexcel(): void {
    /* pass here the table id */
    // let element = document.getElementById('excel-table');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(this.panelList);

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    /* save to file */
    XLSX.writeFile(wb, this.fileName);
  }
}
