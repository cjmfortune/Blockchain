pragma solidity ^0.4.23;

import "./lib/BasicToken.sol";
 
contract WorldCupCryptoBet {    
    using SafeMath for uint256;

    mapping (uint8 => mapping (uint8 => mapping (address => uint256))) public bet;
    mapping (uint8 => mapping (uint8 => uint256 )) public ratio;
    mapping (uint8 => uint256 ) public total;
    mapping (uint8 => uint8) public matchResult;

    string public constant name = "WorldCupCryptoBet"; 
    address public owner_;
    uint8 activeMatch = 0;
    uint256 multiplier = 1;

    constructor() public { 
        owner_ = msg.sender;  
        uint256 decimal = uint256(10) ** 18;
        multiplier = decimal.mul(100000000);
    }    

    modifier onlyOwner() {
        require(msg.sender == owner_);
        _;
    }

    function Bet(uint8 matchNr, uint8 result) public payable {
        require(matchNr > activeMatch);        
        bet[matchNr][result][msg.sender] = msg.value;
        ratio[matchNr][result] += msg.value;
        total[matchNr] += msg.value;
    }

    function nextMach(uint8 matchNr) public onlyOwner{
        activeMatch = matchNr;        
    }

    function setResoultMatch(uint8 matchNr, uint8 result) public onlyOwner{
        matchResult[matchNr] = result;       
    }

    function withdraw(uint8 matchNr) public {
        require(matchResult[matchNr] > 0);
        uint8 result = matchResult[matchNr];
        require(bet[matchNr][result][msg.sender] > 0);

        uint256 ratioVal = bet[matchNr][result][msg.sender].mul(multiplier);
        ratioVal = ratioVal.div(ratio[matchNr][result]);

        uint256 withdrawVal = total[matchNr].mul(ratioVal);
        uint256 valueToWithdraw = withdrawVal.div(multiplier);

        msg.sender.transfer(valueToWithdraw);        
    }
}   