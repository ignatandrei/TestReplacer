import { Component, OnInit } from '@angular/core';
import { MatChipSelectionChange } from '@angular/material/chips';
import { environment } from 'src/environments/environment';
import { ContractData } from '../contract';

class Contract {
  constructor(public name: string) {
    this.DisplayName = name.replace(".docx", "");
    this.DisplayName= this.DisplayName.substring(5).toLocaleUpperCase();
  }
  public DisplayName: string ;
}

@Component({
  selector: 'app-document-single',
  templateUrl: './document-single.component.html',
  styleUrls: ['./document-single.component.css']
})
export class DocumentSingleComponent implements OnInit{
  public contracts: Contract[] = [];
  public NameContract: string = '';
  public replacements: Map<string, string> = new Map<string, string>();
  public isLoading :boolean=false;
  public errMessage:string='';
  constructor(private contractService: ContractData) { }

  ngOnInit(): void {
    this.getContracts();
  }

  
  getContracts(): void {
    this.isLoading=true;
    this.contractService.getDocuments()
    .subscribe(c => {
      this.contracts = c.map(a=>new Contract(a));
      if(this.contracts.length>0)
        this.Load(this.contracts[0]);
    });
  }
  public Save(): void {
    this.contractService.saveReplacements(this.NameContract,this.replacements).subscribe((response: any)  => {

      
        let dataType = response.type;
        let binaryData = [];
        binaryData.push(response);
        let downloadLink = document.createElement('a');
        downloadLink.href = window.URL.createObjectURL(new Blob(binaryData, {type: dataType}));
        downloadLink.setAttribute('download', this.NameContract+'.docx');
        document.body.appendChild(downloadLink);
        downloadLink.click();
    
    });

  }
  public onChangeRep(event: any, key: string): void {
    this.replacements.set(key, event.target.value);
  }
  
  public replArray():string[] {
    return Array.from(this.replacements.keys());
  }

  public Load(contract: Contract): void {
    this.errMessage='';
    this.isLoading=true;
    this.NameContract = contract.DisplayName;
    this.replacements.clear();
    this.contractService.getDocumentReplace(contract.DisplayName)
      .subscribe(
        {
        next: (it) => {
        for (let index = 0; index < it.length; index++) {
          const element = it[index];
          var defValue = '';
          // if(!environment.production){
          //   defValue = 'please replace ' + element;
          // }
          
          this.replacements.set(element, defValue);
        }
      },
      error: (err) => {
        console.log(err);

        var detail='';
        if(err  && err.error && err.error.detail)
          detail  =err.error.detail??'';

        this.errMessage="error loading" + this.NameContract + "=>" + detail;
        window.alert("error \r\n"+ detail);
        this.isLoading=false;
      },
      complete: () => {
        this.isLoading = false;
      }
    }
      );
  }

}
