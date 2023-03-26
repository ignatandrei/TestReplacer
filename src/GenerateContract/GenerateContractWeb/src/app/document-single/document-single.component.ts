import { Component, OnInit } from '@angular/core';
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
  
  constructor(private contractService: ContractData) { }

  ngOnInit(): void {
    this.getContracts();
  }

  
  getContracts(): void {
    this.contractService.getDocuments()
    .subscribe(c => {
      this.contracts = c.map(a=>new Contract(a));
      if(this.contracts.length>0)
        this.Load(this.contracts[0]);
    });
  }
  public Save(): void {
    this.contractService.saveReplacements(this.NameContract,this.replacements).subscribe((it) => {
    });

  }
  public onChangeRep(event: any, key: string): void {
    this.replacements.set(key, event.target.value);
  }

  public replArray():string[] {
    return Array.from(this.replacements.keys());
  }
  public Load(contract: Contract): void {
    this.NameContract = contract.DisplayName;
    this.replacements.clear();
    this.contractService.getDocumentReplace(contract.DisplayName)
      .subscribe((it) => {
        for (let index = 0; index < it.length; index++) {
          const element = it[index];
          var defValue = ''+environment.production;
          if(!environment.production){
            defValue = 'please replace ' + element;
          }
          
          this.replacements.set(element, defValue);
        }
      });
  }

}
