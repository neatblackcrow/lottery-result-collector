class ResultEntryInsert extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            resultEntries: [],
            rewardType: {
                name: "-",
                instance: NaN,
                format: "-",
                reward_amount: "-"
            },
            round: {
                round: "-",
                date: "-"
            }
        };

        this.addResultEntry = this.addResultEntry.bind(this);
        this.testClick = this.testClick.bind(this);
    }

    testClick() {
        this.addResultEntry(1, "123");
    }

    addResultEntry(resultOrder, result) {
        this.setState((prevState, props) => {
            prevState.resultEntries.push({
                resultOrder: resultOrder,
                result: result
            });
            return { resultEntries: prevState.resultEntries };
        });
    }

    componentDidMount() {
        $.ajax({
            url: "GetRoundAndRewardType",
            method: "POST"
        }).success((data, textStatus, jqXHR) => {

            this.setState({
                round: data.round,
                rewardType: data.rewardType
            });
        });
    }

    render() {
        return (
            <div className="container">
                <div className="row">
                    <div className="col-md-3">
                        <RoundBox round={this.state.round} />
                        <RewardTypeBox rewardType={this.state.rewardType} />
                    </div>
                    <div className="col-md-9">
                        <button className="btn btn-primary" onClick={this.testClick}>test</button>
                        <ResultCollection resultEntries={this.state.resultEntries} />
                    </div>
                </div>
            </div>
            );
    }
}