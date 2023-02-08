import { Component } from '@angular/core';
import { ApiCallService } from './services/api-call.service'
import * as XLSX from 'xlsx';

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
  selectedMgr:[]=[];
  strValues:string = '';
  mgrNames:any;
  emptyListMgrId:any;
  mgrNotAvailMsg:any;
  currentPage=1;
  itemsPerPage=10;
  totalPages=3;


  constructor(private trackerItem: ApiCallService) {
    trackerItem.trackerData().subscribe((data) => {
      console.warn('data', data);
      this.item = data;
    })
  }

  validateDate(dateFrom: Date, dateTo: Date) {
    if ((dateFrom.toString() != "" && dateTo.toString() != "" && dateTo > dateFrom)) {
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
      this.availData = data;
      
    })
  }

  getData(date1: any, date2: any) {
    if (this.validateDate(date1, date2)) {
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
      
    })
  }


  getPanelByMultipleMgrs(){
    this.strValues = this.selectedMgr.toString();
    
    console.warn('strValues : ',this.strValues);
    
    this.trackerItem.trackerPanelByMultipleMgrs(this.strValues).subscribe((panel)=>{
      console.warn('Multiple Panel', panel)
      this.panelList = panel;
      if(this.panelList==''){
          this.emptyListMgrId=this.selectedMgr.toString();
          this.trackerItem.trackerEmptyListMgrName(this.emptyListMgrId).subscribe((mgrName)=>{
            console.warn('mgrList',mgrName);
            this.mgrNames=mgrName;            
          })
          this.mgrNotAvailMsg='No Panel Available under'       
      } 
    })

  }

  // radioChangeHandler(event: any) {
  //   this.radioValue = event.target.value;
  // }
  radioChangeHandler(event: any) {    
    this.radioValue = event.target.value; 
    if(this.radioValue == 'byMgr'|| this.radioValue =='byDate' )
    { 
      this.errorMsg=""; 
     this.availData='';
     this.panelList='';
     this.mgrNotAvailMsg='';
     this.mgrNames='';
     this.selectedMgr=[]
    }
  }

  exportexcel(): void {
    /* pass here the table id */
    let element = document.getElementById('excel-table');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    /* save to file */
    XLSX.writeFile(wb, this.fileName);
  }
}
